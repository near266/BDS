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
        public string? Status { get; set; }
        public bool? IsVolatility { get; set; }

    }

    public class WalletDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string? Status { get; set; }
        public bool? IsVolatility { get; set; }

    }
    public class DetailCusDTO
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Company { get; set; }
        public bool? IsUnique { get; set; }
        public string? Avatar { get; set; }
        //Sàn
        public string? Exchange { get; set; }
        public string? ExchangeDescription { get; set; }
        public DateTime? MaintainFrom { get; set; }
        public DateTime? MaintainTo { get; set; }
        public double? Point { get; set; }
        public bool? Status { get; set; }
        public int? TotalSalePost { get; set; }
        public int? TotalBoughtPost { get; set; }
        public string? ReferalCode { get; set; }
        public string? firstName { get; set; }
    }
}
