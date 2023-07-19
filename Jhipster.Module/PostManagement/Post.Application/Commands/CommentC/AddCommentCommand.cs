using AutoMapper;
using MediatR;
using Post.Application.Commands.NotificationC;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Commands.CommentC
{
	public class AddCommentCommand : IRequest<int>
	{
		public string Content { get; set; }
		public string? BoughtPostId { get; set; }
		public string? SalePostId { get; set; }
		[JsonIgnore]
		public int LikeCount { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
		[JsonIgnore]
		public string? CreatedBy { get; set; }
		[JsonIgnore]
		public DateTime? CreatedDate { get; set; }
	}
	public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, int>
	{
		private readonly ICommentRepository _repo;
		private readonly IMapper _mapper;

		public AddCommentCommandHandler(ICommentRepository repository,IMapper mapper)
		{
			_repo = repository;
			_mapper = mapper;
		}
		public async Task<int> Handle(AddCommentCommand request, CancellationToken cancellationToken)
		{
			var map = _mapper.Map<Comment>(request);
			return await _repo.CreateComment(map,cancellationToken);
		}

	}
}
