using System;
using BitCoind.Core.Logic;
using BitCoind.Database;

namespace BitCoind.Logic.Helpers
{
    public class WalletHelper : IWalletHelper
    {
        private readonly BitcoindDbContext _context;

        public WalletHelper(
            BitcoindDbContext context
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}