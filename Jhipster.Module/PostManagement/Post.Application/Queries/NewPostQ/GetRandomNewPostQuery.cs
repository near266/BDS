using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Queries.NewPostQ
{
    public class GetRandomNewPostQuery:IRequest<List<NewPost>>
    {
        public int randomCount { get; set; }
    }
    public class GetRandomNewPostQueryHandler : IRequestHandler<GetRandomNewPostQuery, List<NewPost>>
    {
        private readonly IPostRepository _repository;
        public GetRandomNewPostQueryHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NewPost>> Handle(GetRandomNewPostQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetRandomNewPost(request.randomCount);
        }
    }
}
