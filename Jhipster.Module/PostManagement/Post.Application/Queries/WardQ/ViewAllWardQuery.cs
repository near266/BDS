﻿using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using Post.Application.Contracts;
using Post.Application.DTO;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Queries.WardQ
{
    public class ViewAllWardQuery: IRequest<PagedList<WardDTO>>
    {
        public string? Name { get; set; }
        
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class ViewAllWardQueryHandler : IRequestHandler<ViewAllWardQuery,PagedList<WardDTO>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public ViewAllWardQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedList<WardDTO>> Handle(ViewAllWardQuery request, CancellationToken cancellationToken)
        {
            var res = await _repository.SearchWard(request.Name, request.Page, request.PageSize);
            var map = _mapper.Map<PagedList<WardDTO>>(res);
            return map;
        }
    }
}
