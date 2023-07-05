using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class NewPost : AuditedEntityBase
    {
        [Key]
        public string Id { get; set; } = RandomUtil.GenerateKey();
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? descriptionForList { get; set; }
    }
}
