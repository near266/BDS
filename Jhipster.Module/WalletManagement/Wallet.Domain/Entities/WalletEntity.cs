using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.Domain.Entities
{
	public class WalletEntity : BaseEntity<Guid>
    {

        public string Username { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}

