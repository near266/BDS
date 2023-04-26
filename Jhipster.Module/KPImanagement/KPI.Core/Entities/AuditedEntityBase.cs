using System;
using KPI.Core.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace KPI.Core.Entities
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

