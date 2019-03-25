using System;
using BitCoind.Core.Logic.Enums;

namespace BitCoind.Database.Entities
{
    public class TransactionEntity
    {
        public Guid Id { get; set; }
        public Guid IdempotencyKey { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public DateTime Data { get; set; }
        public byte Confirmation { get; set; }
        public bool IsNew { get; set; }
        public Guid WalletId { get; set; }
        public TransactionType Type { get; set; }
        public virtual WalletEntity Wallet { get; set; }
    }
}