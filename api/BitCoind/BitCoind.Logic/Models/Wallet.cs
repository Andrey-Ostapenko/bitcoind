using System;
using System.Collections.Generic;
using System.Text;

namespace BitCoind.Logic.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public double Balance { get; set; }
        public string Name { get; set; }
    }
}