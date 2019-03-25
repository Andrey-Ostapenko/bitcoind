using System;
using System.Collections.Generic;

namespace BitCoind.Database.Entities
{
    public class WalletEntity
    {
        public WalletEntity()
        {
            Transactions = new HashSet<TransactionEntity>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<TransactionEntity> Transactions { get; set; }
    }
}