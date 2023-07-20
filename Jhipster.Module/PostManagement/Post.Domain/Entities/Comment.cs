using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
	public class Comment : BaseEntity<Guid>
	{
		public string Content { get; set; }
		public int LikeCount { get; set; }	
		public string UserId { get; set; }	
		public string? BoughtPostId { get; set; }	
		public string? SalePostId { get; set; }
		public List<string>? Rely { get; set; }

	}
}
