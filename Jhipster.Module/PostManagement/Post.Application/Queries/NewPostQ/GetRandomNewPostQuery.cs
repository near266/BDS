using MediatR;
using Post.Application.Contracts;
using Post.Application.DTO.NewPostDTO;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Queries.NewPostQ
{
    public class GetRandomNewPostQuery : IRequest<List<NewPoDTO>>
    {
        public int randomCount { get; set; }
    }
    public class GetRandomNewPostQueryHandler : IRequestHandler<GetRandomNewPostQuery, List<NewPoDTO>>
    {
        private readonly IPostRepository _repository;
        public GetRandomNewPostQueryHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NewPoDTO>> Handle(GetRandomNewPostQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetRandomNewPost(request.randomCount);
        }
    }
}
