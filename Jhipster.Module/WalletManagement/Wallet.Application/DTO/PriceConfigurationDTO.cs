using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.DTO
{
    public class PriceConfigurationDTO
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Guid? TypePriceId { get; set; }
        public TypePriceDTO TypePrice { get; set; }
        public int Date { get; set; }
        public decimal? PriceDefault { get; set; }
        public decimal? Discount { get; set; }
        public int? Unit { get; set; }
    }
    public class TypePriceDTO
    {
        public string? Name { get; set; }
    }
    public class PriceConfigDTO
    {
        public int Type { get; set; }
        public List<PriceTypeDTO> PriceConfig { get; set; }
    }
    public class PriceTypeDTO
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Date { get; set; }
        public decimal? PriceDefault { get; set; }
        public decimal? Discount { get; set; }
        public int? Unit { get; set; }
    }
    public class ViewDetailPriceDTO
    {
        public TypePrice TypePrice { get; set; }
        public List<PriceConfigDTO> PriceConfiguration { get; set; }
    }
}
