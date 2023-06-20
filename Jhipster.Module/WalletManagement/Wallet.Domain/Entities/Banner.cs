using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Domain.Entities
{
    public class Banner
    {
        public Guid Id { get; set; }
        public List<string> ListBanner { get; set; }
    }
}
