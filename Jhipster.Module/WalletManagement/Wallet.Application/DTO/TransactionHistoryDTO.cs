using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.DTO
{
    public class SearchTransactionResponse
    {
        public TransactionHistoryDTO TransactionHistorys { get; set; }
        public WalletDto wallet { get; set; }
        public WalletPromotionalDto walletPromotional { get; set; }
        public decimal? WalletAmount { get; set; }
        public decimal? WalletPromotion { get; set; }
        public double? Poin { get; set; }
        public decimal? TotalAmount { get; set; }   

    }
    public class TransactionHistoryDTO
    {
        public Guid Id { get; set; }
        public int? Type { get; set; }

        public string? Content { get; set; }
        public double? TransactionAmount { get; set; }
        //0: Ví chính, 1: Ví khuyến mãi
        public int? WalletType { get; set; }
        public string? Title { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Walletamount { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string Status { get; set; }
        public decimal? Point { get; set; }
    }
}
