using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.DTO.CommentDTO
{
    public class CommentDTO
    {
        public bool IsLike { get; set; }
        public Guid? Id { get; set; }
     

    }
}
