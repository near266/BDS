using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.CommentC
{
	public class DeleteCommentCommand : IRequest<int>
	{
		public Guid Id { get; set; }
	}
	public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand,int>
	{
		private readonly ICommentRepository _repo;

		public DeleteCommentCommandHandler(ICommentRepository repository)
		{
			_repo = repository;
		}

		public async Task<int> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
		{
			return await _repo.DeleteComment(request.Id,cancellationToken);	
		}
	}
}
