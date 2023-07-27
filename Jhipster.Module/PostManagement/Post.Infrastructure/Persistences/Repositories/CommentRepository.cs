using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using Jhipster.Infrastructure.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Post.Application.Contracts;
using Post.Domain.Entities;
using Post.Shared.Dtos;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Post.Infrastructure.Persistences.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private readonly ApplicationDatabaseContext _databaseContext;
		private readonly IMapper _mapper;

		public CommentRepository(ApplicationDatabaseContext databaseContext, IMapper mapper)
		{
			_databaseContext = databaseContext;
			_mapper = mapper;
		}
		public async Task<int> CreateNotification(Notification rq, CancellationToken cancellationToken)
		{
			rq.IsSeen = false;
			rq.CreatedDate = DateTime.Now;
			await _databaseContext.Notification.AddAsync(rq);
			return await _databaseContext.SaveChangesAsync(cancellationToken);
		}
		public async Task<LikeRequest> AddUserLike(Guid? Id, string? userId, CancellationToken cancellationToken)
		{
			var qr = await _databaseContext.Comment.Where(i => i.Id.Equals(Id)).Select(i => i.Rely).FirstOrDefaultAsync();
			var u = await _databaseContext.Comment.Where(i => i.Id.Equals(Id)).Select(i => i.UserId).FirstOrDefaultAsync();
			var like = await _databaseContext.Comment.Where(i => i.Id.Equals(Id)).Select(i => i.LikeCount).FirstOrDefaultAsync();


			var username = await _databaseContext.Customers.Where(i => i.Id.ToString() == userId).Select(i => i.CustomerName).FirstOrDefaultAsync();
			if (qr != null)
			{
				if (qr.Contains(userId) == false)
				{

					if (userId != null)
					{
						qr.Add(userId);
						like += 1;

						var rqNotifi = new Notification();
						rqNotifi.Content = $"{username} đã thích bình luận của bạn";
						rqNotifi.UserId = u;
						await CreateNotification(rqNotifi, cancellationToken);
					}



				}
				else
				{
					if (userId != null)
					{
						qr.Remove(userId);
						like -= 1;

					}
				}
			}

			var res = new LikeRequest
			{
				rely = qr,
				Like = like
			};


			return res;

		}

		public async Task<int> CreateComment(Comment rq, CancellationToken cancellationToken)
		{
			rq.LikeCount = 0;
			await _databaseContext.Comment.AddAsync(rq);
			return await _databaseContext.SaveChangesAsync(cancellationToken);
		}

		public async Task<int> DeleteComment(Guid Id, CancellationToken cancellationToken)
		{
			var check = await _databaseContext.Comment.FirstOrDefaultAsync(i => i.Id == Id);
			if (check == null) throw new Exception("Fail");
			else
			{
				_databaseContext.Comment.Remove(check);
				return await _databaseContext.SaveChangesAsync(cancellationToken);
			}

		}

		public async Task<PagedList<ComentDTO>> GetAllComment(Guid? Id, string? boughtpostId, string? salepostId, int Page, int PageSize, Guid? Userid)
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
				query = query.Where(i => i.SalePostId.Equals(salepostId));
			}

			if (Userid == null)
			{
				query = query;

			}
			var sQuery = query.OrderByDescending(i => i.CreatedDate).Select(i => new ComentDTO
			{
				Id = i.Id,
				BoughtPostId = i.BoughtPostId,
				SalePostId = i.SalePostId,
				UserId = i.UserId,
				Rely = i.Rely,
				LikeCount = i.LikeCount,
				Content = i.Content,
				CreatedDate = i.CreatedDate,
				LastModifiedDate = i.LastModifiedDate,
				Avatar = _databaseContext.Customers.Where(a => a.Id.ToString() == i.UserId).Select(a => a.Avatar).FirstOrDefault(),
				CustomerName = _databaseContext.Customers.Where(a => a.Id.ToString() == i.UserId).Select(a => a.CustomerName).FirstOrDefault(),


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

		public async Task<int> UpdateContent(Guid Id, string? content)
		{
			var check = await _databaseContext.Comment.FirstOrDefaultAsync(i => i.Id == Id);
			if (check == null) throw new Exception("Fail");
			check.Content = content;
			check.LastModifiedBy = check.CreatedBy;
			check.LastModifiedDate = DateTime.UtcNow;
			_databaseContext.Comment.Update(check);
			return await _databaseContext.SaveChangesAsync();
		}
	}
}
