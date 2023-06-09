using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Wallet.Application.DTO
{
    public class WalletDTO
    {
        public Guid CustomerId { get;set; }
        public decimal AmountWallet { get; set; }
        public decimal AmountWalletPromotional { get; set; }
        public string? Currency { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]

        public DateTime? LastModifiedDate { get; set; }

    }
}
