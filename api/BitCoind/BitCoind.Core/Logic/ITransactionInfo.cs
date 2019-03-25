using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BitCoind.Core.Logic
{
    public interface ITransactionInfo
    {
        string Address { get; set; }

        decimal Amount { get; set; }

        Guid IdempotencyKey { get; set; }
    }
}