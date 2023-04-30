using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.CustomerQ
{
    public class ViewDetailCustomerQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }
    }

    public class ViewDetailCustomerQueryHandler : IRequestHandler<ViewDetailCustomerQuery, Customer>
    {
        private readonly ICustomerRepository _repo;
        public ViewDetailCustomerQueryHandler(ICustomerRepository repo)
        {
            _repo = repo;
        }
        public async Task<Customer> Handle(ViewDetailCustomerQuery request, CancellationToken cancellationToken)
        {
            var res = await _repo.GetById(request.Id);
            return res;
        }
    }
}
