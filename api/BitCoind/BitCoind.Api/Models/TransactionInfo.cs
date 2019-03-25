using System;
using System.ComponentModel.DataAnnotations;
using BitCoind.Core.Logic;
using Newtonsoft.Json;

namespace BitCoind.Api.Models
{
    public class TransactionInfo : ITransactionInfo
    {
        [JsonProperty("address", Required = Required.Always)]
        public string Address { get; set; }

        [Range(0.00001, double.PositiveInfinity, ErrorMessage = "Amount should be greater than 0.00001")]
        [JsonProperty("amount", Required = Required.Always)]
        public decimal Amount { get; set; }

        [JsonProperty("idempotency_key", Required = Required.Always)]
        public Guid IdempotencyKey { get; set; }
    }
}