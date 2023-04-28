using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Queries.SalePostQ
{
    public class ViewDeatailSalePostQuery : IRequest<SalePost>
    {
        public string Id { get; set; }
    }
    public class ViewDeatailSalePostQueryHandler : IRequestHandler<ViewDeatailSalePostQuery, SalePost>
    {
        private readonly IPostRepository _Repository;
        private readonly IMapper _mapper;
        public ViewDeatailSalePostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _Repository = repository;
            _mapper = mapper;
        }

        public async Task<SalePost> Handle(ViewDeatailSalePostQuery request, CancellationToken cancellationToken)
        {
            return await _Repository.ViewDetailSalePost(request.Id);
        }
    }
}
