using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class District:AuditedEntityBase
    {
        [Key]
        public string Id { get; set; } = RandomUtil.GenerateKey();
        public string? Name { get; set; }
        public int? Order { get; set; }


    }
}
