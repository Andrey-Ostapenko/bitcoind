using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BitCoind.Core.Logic;
using BitCoind.Core.Logic.Enums;
using BitCoind.Database;
using BitCoind.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace BitCoind.Logic.Helpers
{
    public class HistoryHelper : IHistoryHelper
    {
        private readonly BitcoindDbContext _context;

        public HistoryHelper(
            BitcoindDbContext context
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<object> GetLastAsync(
            CancellationToken ct
        )
        {
            var d = await GetSendListAsync(ct);

            var transactions = await _context.Transactions
                .Where(x => x.IsNew || x.Confirmation < 3)
                .ToListAsync(ct);

            foreach (var transaction in transactions)
            {
                transaction.IsNew = false;
            }

            await _context
                .SaveChangesAsync(ct);

            return transactions
                .Select(Transaction.FromDbo)
                .ToList();
        }

        public async Task<object> GetSendListAsync(
            CancellationToken ct
        )
        {
            var transactions = await _context.Transactions
                .Include(x => x.Wallet)
                .Where(x => x.Type == TransactionType.Send)
                .ToListAsync(ct);

            return transactions; 
        }

        public async Task<object> GetReceiveListAsync(
            CancellationToken ct
        )
        {
            var transactions = await _context.Transactions
                .Include(x => x.Wallet)
                .Where(x => x.Type == TransactionType.Receive)
                .ToListAsync(ct);

            return transactions;
        }
    }
}