using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.TypePriceQ
{
    public class ViewDetailTypePriceQuery : IRequest<ViewDetailPriceDTO>
    {
        public Guid Id { get; set; }
    }
    public class ViewDetailTypePriceQHandler : IRequestHandler<ViewDetailTypePriceQuery, ViewDetailPriceDTO>
    {
        private readonly ITypePriceRepository _repo;
        private readonly IMapper _mapper;

        public ViewDetailTypePriceQHandler(ITypePriceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ViewDetailPriceDTO> Handle(ViewDetailTypePriceQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetPrice(request.Id);
        }
    }
}
