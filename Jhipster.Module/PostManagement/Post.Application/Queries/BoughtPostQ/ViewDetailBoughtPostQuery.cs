using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Application.DTO.SalePostDtos;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Queries.BoughtPostQ
{
    public class ViewDetailBoughtPostQuery : IRequest<BoughtDetail>
    {
        public string Id { get; set; }
    }
    public class ViewDetailBoughtPostQueryHandler : IRequestHandler<ViewDetailBoughtPostQuery, BoughtDetail>
    {
        private readonly IPostRepository _Repository;
        private readonly IMapper _mapper;
        public ViewDetailBoughtPostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _Repository = repository;
            _mapper = mapper;
        }

        public async Task<BoughtDetail> Handle(ViewDetailBoughtPostQuery request, CancellationToken cancellationToken)
        {
            return await _Repository.ViewDetailBoughtPost(request.Id);
        }
    }
}
