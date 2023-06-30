using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Domain.Entities
{
    public class TransactionHistory : BaseEntity<Guid>
    {
        //0: Nạp tiền, 1: Trừ tiền, 2: Hoàn tiền,3:Cộng tiền
        public int? Type { get; set; }
        [MaxLength(50)]
        public string? Content { get; set; }
        public double? TransactionAmount { get; set; }
        //0: Ví chính, 1: Ví khuyến mãi
        public int? WalletType { get; set; }
        [ForeignKey("CustomerId")]
        public Guid? CustomerId { get; set; }
        public string? Title { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Walletamount { get; set; }
        public Customer? Customer { get; set; }
        public string? TransactionCode { get; set; } = RandomUtil.GenerateKey();
    }
}
