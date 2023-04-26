using System;
namespace Wallet.Domain.Entities
{
	public class Wallet : BaseEntity<Guid>
    {
        public string Username { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
    }
}

