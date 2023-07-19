using Jhipster.Crosscutting.Utilities;
using Jhipster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Persistences.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private readonly ApplicationDatabaseContext _databaseContext;
		public CommentRepository(ApplicationDatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		public async Task<int> CreateComment(Comment rq, CancellationToken cancellationToken)
		{
			rq.IsLike = 0;
			rq.CreatedDate = DateTime.Now;
			await _databaseContext.Comment.AddAsync(rq);
			return await _databaseContext.SaveChangesAsync(cancellationToken);
		}

		public async Task<int> DeleteComment(Guid Id, CancellationToken cancellationToken)
		{
			var check = await _databaseContext.Comment.FirstOrDefaultAsync(i => i.Id== Id);
			if (check == null) throw new Exception("Fail");
			else
			{
				_databaseContext.Comment.Remove(check);
				return await _databaseContext.SaveChangesAsync(cancellationToken);
			}
			
		}

		public async Task<PagedList<Comment>> GetAllComment(string Id, int Page, int PageSize)
		{
			var respone = new PagedList<Comment>();
			var data = await _databaseContext.Comment.Where(i => i.UserId == Id).OrderByDescending(i => i.CreatedDate).ToListAsync();
			respone.TotalCount = data.Count;
			respone.Data = data.Skip(PageSize * (Page - 1))
									.Take(PageSize)
									.ToList();
			return respone;
		}

		public async Task<int> UpdateComment(Guid Id, CancellationToken cancellationToken)
		{
			var check = await _databaseContext.Comment.FirstOrDefaultAsync(i => i.Id == Id);
			if (check == null) throw new Exception("Fail");
			else
			{
				_databaseContext.Comment.Update(check);
				return await _databaseContext.SaveChangesAsync(cancellationToken);
			}
		}
	}
}
