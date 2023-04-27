﻿using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Queries.BoughtPostQ
{
    public class ViewAllBoughtPostQuery : IRequest<PagedList<BoughtPost>>
    {
        public int Page { get;set; }
        public int PageSize { get; set; }
    }
    public class ViewAllBoughtPostQueryHandler : IRequestHandler<ViewAllBoughtPostQuery, PagedList<BoughtPost>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public ViewAllBoughtPostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedList<BoughtPost>> Handle(ViewAllBoughtPostQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetBoughtPost(request.Page,request.PageSize);
        }
    }
}
