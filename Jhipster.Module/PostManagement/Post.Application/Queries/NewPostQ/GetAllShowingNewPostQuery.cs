using AutoMapper;
using Jhipster.Crosscutting.Utilities;
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
    public class GetAllShowingNewPostQuery: IRequest<PagedList<NewPost>>
    {
     public string? Title { get; set; } 
     public int Page { get; set; }  
     public int PageSize { get; set; }   

    }
    public class GetAllShowingNewPostQueryHandler : IRequestHandler<GetAllShowingNewPostQuery, PagedList<NewPost>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public GetAllShowingNewPostQueryHandler(IPostRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;   
        }
        public async Task<PagedList<NewPost>> Handle(GetAllShowingNewPostQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetShowingNewPost(request.Title,request.Page,request.PageSize);
        }
    }
}
