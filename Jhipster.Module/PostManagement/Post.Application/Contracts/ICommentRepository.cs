using Jhipster.Crosscutting.Utilities;
using Post.Domain.Entities;
using Post.Shared.Dtos;
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
		Task<PagedList<ComentDTO>> GetAllComment(Guid? Id,string? boughtpostId,string? salepostId, int Page, int PageSize,Guid? Userid);
		Task<List<string>> AddUserLike( Guid? Id,string? userId);
		
	}
}
