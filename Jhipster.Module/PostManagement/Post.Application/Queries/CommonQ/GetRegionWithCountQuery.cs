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
    public class GetRegionWithCountQuery : IRequest<List<PostDto>>
    {
        public int? Type { get; set; }
    }
    public class GetRegionWithCountQueryHandler : IRequestHandler<GetRegionWithCountQuery, List<PostDto>>
    {
        private readonly IPostRepository _postRepository;
        public GetRegionWithCountQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<List<PostDto>> Handle(GetRegionWithCountQuery request, CancellationToken cancellationToken)
        {
            var res = await _postRepository.GetAllRegion(request.Type);
            return res;
        }
    }
}
