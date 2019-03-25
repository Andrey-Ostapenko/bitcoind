using System;
using System.Threading;
using System.Threading.Tasks;
using BitCoind.Core.Logic;
using BitCoind.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BitCoind.Api.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITokenHelper _tokenHelper;

        public ApiController(
            ILogger<ApiController> logger,
            ITokenHelper tokenHelper
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tokenHelper = tokenHelper ?? throw new ArgumentNullException(nameof(tokenHelper));
        }

        protected async Task<ITokenInfo> Token(CancellationToken ct = default(CancellationToken))
        {
            return await _tokenHelper.Check(GetTokenFromCookies(), ct);
        }

        private static ObjectResult NotAllowedMethod(ApiException e)
        {
            return new ObjectResult(e.AsError()) {StatusCode = 405};
        }

        private static ObjectResult ExceptionOccur(ApiErrorCode code, Exception e)
        {
            return new ObjectResult(new ApiError(code, e.Message)) {StatusCode = 500};
        }

        [NonAction]
        private string GetTokenFromCookies()
        {
            return Request.Cookies["token"];
        }

        [NonAction]
        protected async Task<IActionResult> Exec(
            Func<ITokenInfo, Task<object>> method,
            ApiErrorCode code,
            CancellationToken ct
        )
        {
            var info = await Token(ct);
            if (info == null)
            {
                _logger.Log(LogLevel.Error, new NotAuthorizedException(GetTokenFromCookies(), code),
                    code.ToString());
                return new UnauthorizedResult();
            }

            try
            {
                return Ok(await method(info));
            }
            catch (NotAllowedException e)
            {
                _logger.Log(LogLevel.Error, e, e.Code.ToString());
                return NotAllowedMethod(e);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, code.ToString());
                return ExceptionOccur(code, e);
            }
        }       
    }
}