using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Commands.AdminC
{
    public class ApprovePostCommand : IRequest<int>
    {
        public int postType { get; set; }
        public List<string> listId { get; set; }
        public int status { get; set; }
        public string? reason { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
    }
    public class ApprovePostCommandHandler : IRequestHandler<ApprovePostCommand, int>
    {
        private readonly IPostRepository _postRepository;
        public ApprovePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<int> Handle(ApprovePostCommand request, CancellationToken cancellationToken)
        {
            return await _postRepository.ApprovePost(request.postType, request.listId, request.status, request.reason, request.LastModifiedDate, request.LastModifiedBy, cancellationToken);
        }
    }
}
