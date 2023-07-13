using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jhipster.Dto
{
    public class RqGetAllUserDTO
    {
        public string? phone { get; set; }
        public string? username { get; set; }
        public int page { get; set; }
        public int pagesize { get; set; }
    }
}
