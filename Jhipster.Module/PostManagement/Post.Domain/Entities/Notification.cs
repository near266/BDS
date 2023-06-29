using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class Notification : BaseEntity<Guid>
    {
        public string Content { get; set; }
        public bool IsSeen { get; set; }
        public string UserId { get; set; }
    }
}
