using Jhipster.Crosscutting.Utilities;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts
{
	public interface ICommentRepository
	{
		Task<int> CreateComment(Comment rq,CancellationToken cancellationToken);
		Task<int> UpdateComment(Comment rq,CancellationToken cancellationToken);
		Task<int> DeleteComment(Guid Id, CancellationToken cancellationToken);
		Task<PagedList<Comment>> GetAllComment(string? Id,string? boughtpostId,string? salepostId, int Page, int PageSize);
	}
}
