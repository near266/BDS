using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using Jhipster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Post.Application.Contracts;
using Post.Domain.Entities;
using Post.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Persistences.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private readonly ApplicationDatabaseContext _databaseContext;
		private readonly IMapper _mapper;

		public CommentRepository(ApplicationDatabaseContext databaseContext,IMapper mapper)
		{
			_databaseContext = databaseContext;
			_mapper = mapper;
		}

        public async Task<List<string>> AddLike(string? userId, string? postId, string? boughtId)
        {
			var qr = await _databaseContext.Comment.Where(i=>i.SalePostId==postId || i.BoughtPostId==boughtId).FirstOrDefaultAsync();
		if(qr == null)
			{
				throw new ArgumentNullException("not found");
			}
			List<string> user = new List<string>();
			user.Add(userId);
			
			return user;
		  
        }

        public async Task<int> CreateComment(Comment rq, CancellationToken cancellationToken)
		{
			rq.LikeCount = 0;
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

		public async Task<PagedList<ComentDTO>> GetAllComment(string? Id, string? boughtpostId, string? salepostId, int Page, int PageSize,Guid? Userid)
		{
			var query = _databaseContext.Comment.AsQueryable();

			if (Id != null)
			{
				query = query.Where(i => i.Id.Equals(Id));
			}
			if (boughtpostId != null)
			{
				query = query.Where(i => i.BoughtPostId.Equals(boughtpostId));
			}
			if (salepostId != null)
			{
				query = query.Where(i => i.BoughtPostId.Equals(boughtpostId));
			}

		if(Userid == null)
			{
                query = query;

            }
            var sQuery = query.OrderByDescending(i => i.CreatedDate).Select(i=>new ComentDTO
			{
				BoughtPostId = i.BoughtPostId,
				UserId= Userid.ToString(),
				LikeCount=i.LikeCount,
				Content=i.Content,
                Avatar = _databaseContext.Customers.Where(a=>a.Id.ToString()==i.UserId).Select(a=>a.Avatar).FirstOrDefault() ,
				CustomerName= _databaseContext.Customers.Where(a=>a.Id.ToString() == i.UserId).Select(a=>a.CustomerName).FirstOrDefault(),


			});
			var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
										.Take(PageSize)
										.ToListAsync();
			var reslist = await sQuery.ToListAsync();

			return new PagedList<ComentDTO>
			{
				Data = sQuery1,
				TotalCount = reslist.Count,
			};
		}

		public async Task<int> UpdateComment(Comment rq, CancellationToken cancellationToken)
		{
			var check = await _databaseContext.Comment.FirstOrDefaultAsync(i => i.Id == rq.Id);
			if (check == null) throw new Exception("Fail");
			else
			{
				_mapper.Map(rq, check);
				return await _databaseContext.SaveChangesAsync(cancellationToken);
			}
		}
	}
}
