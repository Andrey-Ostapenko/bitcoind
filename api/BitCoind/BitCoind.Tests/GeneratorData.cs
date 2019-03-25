using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BitCoind.Database;
using BitCoind.Database.Entities;
using BitCoind.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin;

namespace BitCoind.Tests
{
    [TestClass]
    public class GeneratorData : DbContext
    {
        [TestMethod]
        public async Task GeneratorTransaction()
        {
            var cancelTokenSource = new CancellationTokenSource();
            var ct = cancelTokenSource.Token;

            var rnd = new Random();

            using (var context = new BitcoindDbContext(DbContextOptions))
            {
                for (var i = 1; i < 1000; i++)
                {
                    var bitcoin = new Key();
                    var bitcoinaddress = bitcoin.PubKey.GetAddress(Network.TestNet);
                    var amount = rnd.Next(1, i);
                    var confirmation = (byte)rnd.Next(0, 4);

                    var transaction = new TransactionEntity
                    {
                        Address = bitcoinaddress.ToString(),
                        Amount = amount,
                        IdempotencyKey = Guid.NewGuid(),
                        Data = DateTime.UtcNow,
                        Confirmation = confirmation
                    };

                    await context
                        .Transactions
                        .AddAsync(transaction, ct);
                }

                await context
                    .SaveChangesAsync(ct);
            }
        }
    }
}
