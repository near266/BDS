using Jhipster.Crosscutting.Utilities;
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
    public class SearchCustomerQuery : IRequest<PagedList<Customer>>
    {
        public int page { get; set; }
        public int pagesize { get; set; }
    }

    public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery, PagedList<Customer>>
    {
        private readonly ICustomerRepository _repo;
        public SearchCustomerQueryHandler(ICustomerRepository repo)
        {
            _repo = repo;
        }
        public async Task<PagedList<Customer>> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
        {
            var res = await _repo.Search(request.page, request.pagesize);
            return res;
        }
    }
}
