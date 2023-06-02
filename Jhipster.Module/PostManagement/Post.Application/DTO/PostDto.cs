using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.DTO
{
    public class PostDto
    {
        public string Region { get; set; }
        public int Count { get; set; }
    }

    public class StatusDto
    {
        public int Status { get; set; }
        public int Count { get; set; }
    }
}
