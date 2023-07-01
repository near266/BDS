using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post.Domain.Entities
{
    public class SalePost : AuditedEntityBase
    {
        [Key]
        public string Id { get; set; } = RandomUtil.GenerateKey();

        public int Type { get; set; }

        [MaxLength(500)]
        public string? Titile { get; set; }
        [MaxLength(3000)]
        public string? Description { get; set; }
        /*
            1 : Đang hiển thị
            0 : Chưa duyệt
            2 : Từ chối
            3 : Đã bán
            4 : Hạ
       */
        public int Status { get; set; }
        public double Price { get; set; }
        //Diện tích
        public double? Area { get; set; }
        [MaxLength(100)]
        public string? Region { get; set; }
        [MaxLength(100)]
        public string? Ward { get; set; }
        public List<string>? Image { get; set; }
        public int IsOwner { get; set; }
        public string Username { get; set; }
        [MaxLength(50)]
        public string? FullName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MaxLength(250)]
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DueDate { get; set; }
        public string UserId { get; set; }
        public List<string>? Documents { get; set; }
        public string? Reason { get; set; }
        public DateTime? Order { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public int Unit { get; set; }     // 0:VND , 1:VND/m2
        public Guid? PriceId { get; set; }

    }

    public static class RandomUtil
    {
        private static readonly Random random = new Random();

        public static string GenerateKey()
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}

