using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Wallet.Application.DTO
{
    public class BannerDTO
    {
        public Guid Id { get; set; }
        public List<string> ListBanner { get; set; }

    }
}
