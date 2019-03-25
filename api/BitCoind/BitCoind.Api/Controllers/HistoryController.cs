using System;
using System.Threading;
using System.Threading.Tasks;
using BitCoind.Core.Logic;
using BitCoind.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BitCoind.Api.Controllers
{
    [ApiController]
    [Route("api/history")]
    public class HistoryController : ApiController
    {
        private readonly IHistoryHelper _historyHelper;

        public HistoryController(
            ILogger<HistoryController> logger,
            ITokenHelper tokenHelper,
            IHistoryHelper historyHelper
        ) : base(logger, tokenHelper)
        {
            _historyHelper = historyHelper ?? throw new ArgumentNullException(nameof(historyHelper));
        }


        [HttpGet]
        public async Task<IActionResult> GetLast(CancellationToken ct)
        {
            return await Exec(token => _historyHelper.GetLastAsync(ct),
                ApiErrorCode.HistoryGetLast,
                ct
            );
        }
    }
}