using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Domain.Entities
{
    public class TypeTerm : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string? DetailTerm { get;set; }
        public IEnumerable <TermConditionConfiguration>? TermConfig { get; set; }
    }
}
