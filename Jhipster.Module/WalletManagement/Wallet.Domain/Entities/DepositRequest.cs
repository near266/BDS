using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Domain.Entities
{
    public class DepositRequest : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public int Status { get; set; } // 0: Chưa xử lý , 1: Đã xác nhận , 2:Hủy bỏ
        public Customer Customer { get; set; }  
       
    }
}
