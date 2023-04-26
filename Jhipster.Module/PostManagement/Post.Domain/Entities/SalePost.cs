using System;
using System.ComponentModel.DataAnnotations;

namespace Post.Domain.Entities
{
	public class SalePost : AuditedEntityBase
	{
        [Key]
        public string Id { get; set; } = RandomUtil.GenerateKey();

        public int Type { get; set; }

        [MaxLength(25)]
        public string? Titile { get; set; }
        [MaxLength(3000)]
        public string? Description { get; set; }

        public List<string>? Image { get; set; }

        public int IsOwner { get; set; }

        public string Username { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }
    }

    public class RandomUtil
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

