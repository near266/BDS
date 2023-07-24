using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.CommentC
{
    public class UpdateContentCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
    }
    public class UpdateContentCommandHandler : IRequestHandler<UpdateContentCommand, int>
    {
        private readonly ICommentRepository _repo;
        public UpdateContentCommandHandler(ICommentRepository repository)
        {
            _repo = repository;
        }

        public async Task<int> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            return await _repo.UpdateContent(request.Id,request.Content);
        }
    }
}
