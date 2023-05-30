using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Domain.Entities
{
    public class Customer : BaseEntity<Guid>
    {
        [MaxLength(100)]
        public string CustomerName { get; set; }
        [MaxLength(500)]
        public string? Address { get; set; }
        [Phone]
        public string? Phone { get; set; }
        [MaxLength(100)]
        public string? Company { get; set; }
        public bool? IsUnique { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Avatar { get; set; }
        //Sàn
        [MaxLength(100)]
        public string? Exchange { get; set; }
        [MaxLength(int.MaxValue)]
        public string? ExchangeDescription { get; set; }
        public DateTime? MaintainFrom { get; set; }
        public DateTime? MaintainTo { get; set; }
        public double? Point { get; set; }
        public bool? Status { get; set; }
    }
}
