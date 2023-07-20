using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Commands.CommentC
{
    public class AddUserLikeCommand : IRequest<List<string>>
    {
        public Guid? Id { get; set; }
        [JsonIgnore]
        public string? userId { get; set; } 

    }
    public class AddUserLikeCommandHandler : IRequestHandler<AddUserLikeCommand, List<string>>
    {
        private readonly ICommentRepository _repo;
        public AddUserLikeCommandHandler(ICommentRepository repository)
        {
            _repo = repository;
        }

        public async Task<List<string>> Handle(AddUserLikeCommand request, CancellationToken cancellationToken)
        {
            return await _repo.AddUserLike(request.Id,request.userId,cancellationToken);
        }
    }
}
