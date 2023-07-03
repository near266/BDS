using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Domain.Entities
{
    public class TermConditionConfiguration : BaseEntity<Guid>
    {

        public string? Title { get; set; }
        public string? Description { get; set; }

        [ForeignKey("TypeTermId")]
        public Guid? TypeTermId { get; set; }
        public TypeTerm? TypeTerm { get; set; }
    }
}
