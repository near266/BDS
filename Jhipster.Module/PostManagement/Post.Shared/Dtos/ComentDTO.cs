using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Shared.Dtos
{
    public class ComentDTO
    {
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public string? UserId { get; set; }
        public string? BoughtPostId { get; set; }
        public string? SalePostId { get; set; }
        public List<string>? Rely { get; set; }
        public string? CustomerName { get; set; }
        public string? Avatar { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }


    }

    public class LikeRequest
    {
        public string? UserId { get; set; }
        public string? postId { get; set; }
        public string? boughtId { get; set; }
    }
}
