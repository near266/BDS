using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.CustomerQ
{
    public class ViewDetailCustomerQuery : IRequest<DetailCusDTO>
    {
        public Guid Id { get; set; }
    }

    public class ViewDetailCustomerQueryHandler : IRequestHandler<ViewDetailCustomerQuery, DetailCusDTO>
    {
        private readonly ICustomerRepository _repo;
        public ViewDetailCustomerQueryHandler(ICustomerRepository repo)
        {
            _repo = repo;
        }
        public async Task<DetailCusDTO> Handle(ViewDetailCustomerQuery request, CancellationToken cancellationToken)
        {
            var res = await _repo.GetById(request.Id);
            return res;
        }
    }
}
