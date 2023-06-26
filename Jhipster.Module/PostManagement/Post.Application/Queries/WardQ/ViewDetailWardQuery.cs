using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using Post.Application.Contracts;
using Post.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Queries.WardQ
{
    public class ViewDetailWardQuery : IRequest<WardDtos>
    {
        public string? Id { get; set; }
    }
    public class ViewDetailWardQueryHandler : IRequestHandler<ViewDetailWardQuery, WardDtos>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public ViewDetailWardQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WardDtos> Handle(ViewDetailWardQuery request, CancellationToken cancellationToken)
        {
            var res = await _repository.GetDetailWard(request.Id);
            var map = _mapper.Map<WardDtos>(res);
            return map;
        }
    }
}
