﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Domain.Entities
{
    public class PriceConfiguration : BaseEntity<Guid>
    {
        public int Type { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Guid? TypePriceId { get; set; }
        public TypePrice TypePrice { get; set; }
        public int Date { get; set; }

        public decimal? PriceDefault { get; set; }  
        public decimal? Discount { get; set; }
        public int? Unit { get; set; }     // 0: vnd , 1:vnd/m2

    }
}
