using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.DTO
{
    public class SearchCustomerReponse
    {
        public List<CustomerDetail> Data { get; set; }
        public int TotalCount { get; set; }
    }

    public class CustomerDetail
    {
        public Customer customer { get; set; }
        public WalletDto wallet { get; set; }
        public WalletPromotionalDto walletPromotional { get; set; }
    }

    public class WalletPromotionalDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
    }

    public class WalletDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
    }
}
