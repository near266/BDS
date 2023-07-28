using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.DTO.NewPostDTO
{
    public class NewPoDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public string? descriptionForList { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
		public string? LastModifiedBy { get; set; }
		public DateTime? LastModifiedDate { get; set; }


	}
}
