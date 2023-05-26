using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Application.DTO
{
    public class WalletResponseDTO
    {
        public WalletDto? wallet { get; set; }
        public WalletPromotionalDto? walletPromotional { get; set; }
        public CustomerDto? customer { get; set; }
    }

    public class CustomerDto
    {
        public double? Point { get; set; }
    }
}
