using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Jhipster.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Jhipster.Domain
{
    public class User : IdentityUser, IAuditedEntityBase
    {
        public string Login
        {
            get => UserName;
            set => UserName = value;
        }

        [StringLength(30)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required] public bool Activated { get; set; }

        [StringLength(6, MinimumLength = 2)]
        [Column("lang_key")]
        public string LangKey { get; set; }

        [Url]
        [StringLength(256)]
        [Column("image_url")]
        public string ImageUrl { get; set; }

        [StringLength(20)]
        [Column("activation_key")]
        [JsonIgnore]
        public string ActivationKey { get; set; } = RandomUtil.GenerateKey();

        [StringLength(20)]
        [Column("reset_key")]
        [JsonIgnore]
        public string ResetKey { get; set; }

        [Column("reset_date")] public DateTime? ResetDate { get; set; }

        [JsonIgnore] public virtual ICollection<UserRole> UserRoles { get; set; }

        public string Company { get; set;}
        public string Address { get; set; }
        public string? OTP { get; set; } = RandomUtil.GenerateKey();
        public string? ReferalCode { get; set; }
        public bool? IsEnterprise { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            if (obj == null || GetType() != obj.GetType()) return false;

            var user = obj as User;
            if (user?.Id == null || Id == null) return false;

            return EqualityComparer<string>.Default.Equals(Id, user.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return "User{" +
                   $"ID='{Id}'" +
                   $", Login='{Login}'" +
                   $", FirstName='{FirstName}'" +
                   $", LastName='{LastName}'" +
                   $", Email='{Email}'" +
                   $", ImageUrl='{ImageUrl}'" +
                   $", Activated='{Activated}'" +
                   $", LangKey='{LangKey}'" +
                   $", ActivationKey='{ActivationKey}'" +
                   "}";
        }
        public static class RandomUtil
        {
            private static readonly Random random = new Random();

            public static string GenerateKey()
            {
                const string chars = "0123456789";
                return new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
    }
}
