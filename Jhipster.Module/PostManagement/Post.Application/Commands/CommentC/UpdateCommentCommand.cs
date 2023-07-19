using AutoMapper;
using MediatR;
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
	public class UpdateCommentCommand : IRequest<int>
	{
		public Guid Id { get; set; }
		public int? IsLike { get; set; }	
		public string? Content { get; set; }
		public string? BoughtPostId { get; set; }
		public string? SalePostId { get; set; }

		[JsonIgnore]
        public string? LastModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
	}
	public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, int>
	{
		private readonly ICommentRepository _repo;
		private readonly IMapper _mapper;

		public UpdateCommentCommandHandler(ICommentRepository repository, IMapper mapper)
		{

			_repo = repository;
			_mapper = mapper;
		}

		public async Task<int> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
		{
			var map = _mapper.Map<Comment>(request);
			return await _repo.UpdateComment(map, cancellationToken);
		}
	}
}
