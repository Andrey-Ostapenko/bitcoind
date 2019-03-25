using System;
using System.Threading;
using System.Threading.Tasks;
using BitCoind.Core.Logic;
using BitCoind.Core.Logic.Enums;
using BitCoind.Database;
using BitCoind.Database.Entities;
using BitCoind.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace BitCoind.Logic.Helpers
{
    public class TransferHelper : ITransferHelper
    {
        private readonly BitcoindDbContext _context;

        public TransferHelper(
            BitcoindDbContext context
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<object> SendBtcAsync(
            ITransactionInfo info,
            CancellationToken ct
        )
        {
            var wallet = await _context
                .Wallets
                .FirstOrDefaultAsync(e => e.Balance > info.Amount, ct);

            if (wallet == null)
                throw new Exception("Wallet not found for the transaction");

            var transaction = new TransactionEntity
            {
                Address = info.Address,
                Amount = info.Amount,
                IdempotencyKey = info.IdempotencyKey,
                Data = DateTime.UtcNow,
                WalletId = wallet.Id,
                Type = TransactionType.Send
            };

            await _context
                .Transactions
                .AddAsync(transaction, ct);

            await _context
                .SaveChangesAsync(ct);

            return Transaction.FromDbo(transaction);
        }
    }
}