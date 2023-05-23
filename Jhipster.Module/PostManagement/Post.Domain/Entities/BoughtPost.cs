using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post.Domain.Entities
{
    public class BoughtPost : AuditedEntityBase
    {
        [Key]
        public string Id { get; set; } = RandomUtil.GenerateKey();
        [MaxLength(500)]
        public string? Titile { get; set; }
        [MaxLength(3000)]
        public string? Description { get; set; }
        public string? LandToBuy { get; set; }
        public string? Criteria { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        [MaxLength(250)]
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MaxLength(100)]
        public string? Region { get; set; }
        public double Price { get; set; }
        public bool? IsOpenFinance { get; set; }
        //đơn vị : triệu, tỷ, . . .
        public string? Unit { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public string? Reason { get; set; }
    }
}

