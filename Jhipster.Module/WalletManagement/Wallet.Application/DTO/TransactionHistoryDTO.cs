using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.DTO
{
    public class SearchTransactionResponse
    {
        public TransactionHistory TransactionHistory { get; set; }
        public WalletDto wallet { get; set; }
        public WalletPromotionalDto walletPromotional { get; set; }

    }
}
