using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jhipster.Domain.Entities
{
    [Table("DeliveryDatas")]
    public class DeliveryData : BaseEntity<Guid>
    {
        [Description("Dữ liệu cần chuyển đi: OTP, Password Temp, Username, . . .")]
        public string Data { get; set; }
        [MaxLength(100)]
        public string Method { get; set; }
        [MaxLength(1000)]
        public string MethodData { get; set; }
        public string? Subject { get; set; }
        public int Type { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsSend { get; set; }
    }
}
