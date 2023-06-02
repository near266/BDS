using MediatR;
using Post.Application.Contracts;
using Post.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Queries.CommonQ
{
    public class GetStatusWithCountQuery : IRequest<List<StatusDto>>
    {
        public int? Type { get; set; }
        public string UserId { get; set; }
    }
    public class GetStatusWithCountQueryHandler : IRequestHandler<GetStatusWithCountQuery, List<StatusDto>>
    {
        private readonly IPostRepository _postRepository;
        public GetStatusWithCountQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<List<StatusDto>> Handle(GetStatusWithCountQuery request, CancellationToken cancellationToken)
        {
            return await _postRepository.GetAllStatus(request.Type, request.UserId);
        }
    }
}
