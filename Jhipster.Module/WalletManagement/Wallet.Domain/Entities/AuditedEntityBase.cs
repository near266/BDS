using System;
using System.ComponentModel.DataAnnotations;
using Wallet.Domain.Entities.Interfaces;

namespace Wallet.Domain.Entities
{
    public abstract class AuditedEntityBase : IAuditedEntityBase
    {
        [MaxLength(100)]
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(100)]
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}

