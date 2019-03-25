using System;
using BitCoind.Database.Entities;

namespace BitCoind.Logic.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid IdempotencyKey { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public DateTime Data { get; set; }
        public byte Confirmation { get; set; }

        private Transaction(TransactionEntity transaction)
        {
            Id = transaction.Id;
            IdempotencyKey = transaction.IdempotencyKey;
            Address = transaction.Address;
            Amount = transaction.Amount;
            Data = transaction.Data;
            Confirmation = transaction.Confirmation;
        }

        public static Transaction FromDbo(TransactionEntity transaction) =>
            transaction == null
                ? null
                : new Transaction(transaction);
    }
}