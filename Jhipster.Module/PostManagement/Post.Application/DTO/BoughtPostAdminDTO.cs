using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.DTO
{
    public class BoughtPostAdminDTO
    {
        public string Id { get; set; } 
        public string? Titile { get; set; }
        public string? Description { get; set; }
        public List<string>? Image { get; set; }
        public string? LandToBuy { get; set; }
        public string? Criteria { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Region { get; set; }
        public string? Ward { get; set; }
        public double Price { get; set; }
        public bool? IsOpenFinance { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public string? Reason { get; set; }
        public DateTime? Order { get; set; }
        public double? PriceTo { get; set; }
        public string? RangePrice { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? AddressUser { get; set; }
    }
}
