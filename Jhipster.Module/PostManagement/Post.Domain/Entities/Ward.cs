using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class Ward:AuditedEntityBase
    {
        [Key]
        public string Id { get; set; } = RandomUtil.GenerateKey();
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int? Order { get; set; }
        [MaxLength(300)]
        public string? Description { get; set; }
        [ForeignKey("DistrictId")]
        public string? DistrictId { get; set; }
        public District? District { get; set; }
    }
}
