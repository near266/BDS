using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class Ward:AuditedEntityBase
    {
        [Key]
        public string Id { get; set; } = RandomUtil.GenerateKey();
        public string Name { get; set; }
        public District District { get; set; }

        public string DistrictId { get; set; }
    }
}
