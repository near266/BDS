using AutoMapper;
using Jhipster.Crosscutting.Utilities;
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
    public class ViewAllNewPostQuery : IRequest<PagedList<NewPoDTO>>
    {
        public string? Title { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
    public class ViewAllNewPostQueryHandler : IRequestHandler<ViewAllNewPostQuery, PagedList<NewPoDTO>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public ViewAllNewPostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedList<NewPoDTO>> Handle(ViewAllNewPostQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SearchNewPost(request.Title, request.Page, request.PageSize);
        }
    }
}
