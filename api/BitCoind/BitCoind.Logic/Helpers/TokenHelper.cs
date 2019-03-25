using System;
using System.Threading;
using System.Threading.Tasks;
using BitCoind.Core.Logic;
using BitCoind.Logic.Models;
using Microsoft.Extensions.Logging;

namespace BitCoind.Logic.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        private readonly ILogger<TokenHelper> _logger;

        public TokenHelper(
            ILogger<TokenHelper> logger
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ITokenInfo> Check(
            string token = "test",
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            try
            {
                // TODO: make logic to check a token
                var info = new TokenInfo();

                return info;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return null;
        }
    }
}