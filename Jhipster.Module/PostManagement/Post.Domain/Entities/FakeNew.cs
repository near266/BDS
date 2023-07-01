using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class FakeNew : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public int Order { get; set; }
        public int OrderMax { get; set; }
    }
}
