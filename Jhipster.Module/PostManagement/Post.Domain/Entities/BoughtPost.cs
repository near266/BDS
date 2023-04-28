using Jhipster.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post.Domain.Entities
{
	public class BoughtPost : AuditedEntityBase
	{
        [Key]
        public string Id { get; set; } = RandomUtil.GenerateKey();

        public string? LandToBuy { get; set; }
        public string? Criteria { get; set; }

        public string? FullName { get; set; }

        public string Username { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public int Status { get; set; }
        public string UserId { get; set; }
    }
}

