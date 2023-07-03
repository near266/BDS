using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Domain.Entities
{
    public class TypePrice : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public IEnumerable<PriceConfiguration> priceConfigurations { get; set; }
    }
}
