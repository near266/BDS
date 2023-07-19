using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Queries.CommentQ
{
	public class GetAllCommentQuery : IRequest<PagedList<Comment>>
	{
		public string? Id { get; set; }
		public string? BoughtPostId { get; set; }
		public string? SalePostId { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
	public class GetAllCommentQueryHandler : IRequestHandler<GetAllCommentQuery, PagedList<Comment>>
	{
		private readonly ICommentRepository _repo;
		private readonly IMapper _mapper;

		public GetAllCommentQueryHandler(ICommentRepository repo,IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;

		}
		public async Task<PagedList<Comment>> Handle(GetAllCommentQuery request, CancellationToken cancellationToken)
		{
			return await _repo.GetAllComment(request.Id,request.BoughtPostId,request.SalePostId,request.Page,request.PageSize);	
		}
	}
}
