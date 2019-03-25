using System;
using System.Threading;
using System.Threading.Tasks;
using BitCoind.Core.Logic;
using BitCoind.Exceptions;
using BitCoind.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionInfo = BitCoind.Api.Models.TransactionInfo;

namespace BitCoind.Api.Controllers
{
    [ApiController]
    [Route("api/transfer")]
    public class TransferController : ApiController
    {
        private readonly ITransferHelper _transferHelper;

        public TransferController(
            ILogger<TransferController> logger,
            ITokenHelper tokenHelper,
            ITransferHelper transferHelper
        ) : base(logger, tokenHelper)
        {
            _transferHelper = transferHelper ?? throw new ArgumentNullException(nameof(transferHelper));
        }

        [HttpPut("send-btc")]
        public async Task<IActionResult> SendBtc(
            [FromBody] TransactionInfo parameter,
            CancellationToken ct
        )
        {
            return await Exec(token => _transferHelper.SendBtcAsync(parameter, ct),
                ApiErrorCode.TransferSendBtc,
                ct
            );
        }
    }
}