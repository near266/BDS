using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Application.DTO
{
    public class AddPriceDTO
    {
        public string Name { get; set; }
        public List<TypeDTO> Config { get; set; }
    }
    public class UpdatePriceDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TypeDTO> Config { get; set; }
    }
    public class TypeDTO
    {
        public List<TypePriDTO> TypePri { get; set; }
        public decimal PriceDefault { get; set; }

        public int Type { get; set; }
    }
    public class TypePriDTO
    {
        public string? Description { get; set; }
        public int Date { get; set; }
        public decimal? Discount { get; set; }
        public int? Unit { get; set; }
    }
}
