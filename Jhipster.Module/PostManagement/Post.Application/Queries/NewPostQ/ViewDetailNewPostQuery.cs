using AutoMapper;
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
    public class ViewDetailNewPostQuery: IRequest<NewPost>
    {
        public string Id { get; set; }
    }
    public class ViewDetailNewPostQueryHandler :IRequestHandler<ViewDetailNewPostQuery,NewPost>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public ViewDetailNewPostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<NewPost> Handle(ViewDetailNewPostQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ViewDetailNewPost(request.Id);
        }
    }
    
}
