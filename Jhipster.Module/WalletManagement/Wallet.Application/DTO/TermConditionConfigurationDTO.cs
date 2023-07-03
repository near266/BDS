using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.DTO
{
    public class TermConditionConfigurationDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? TypeTermId { get; set; }
        public TypeTerm? TypeTerm { get; set; }
    }
    public class TypeTermDTO
    {
        public string? Name { get; set;}
    }
    public class TermConditionConfigDTO
    {
        public List<TermConditionDTO> TermCondition { get; set; }
    }
    public class TermConditionDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
    public class ViewDetailTermConditionDTO
    {
        public TypeTerm TypeTerm { get; set; }  
        public List<TermConditionConfigDTO> TermConditionConfiguration { get; set; }
    } 
}
