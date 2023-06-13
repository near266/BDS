using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Shared.Enums
{
    public enum PostStatus
    {
        /*
            1 : Đang hiển thị
            0 : Chưa duyệt
            2 : Từ chối
            3 : Đã bán
            4 : Hạ
            5 : Hết hạn
            6 : Đã mua

            
       */
        Showing = 1,
        UnApproved = 0,
        Rejected = 2,
        Sold = 3,
        Down = 4,
        OffDate = 5,
        Bought = 6,

    }
}
