using Jhipster.Crosscutting.Utilities;
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
    public class SearchCustomerQuery : IRequest<SearchCustomerReponse>
    {
        public string? CustomerCode { get; set; }
        public string? keyword { get; set; }
        public string? phone { get; set; }
        public bool? isUnique { get; set; }
        public int page { get; set; }
        public int pagesize { get; set; }
    }

    public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery, SearchCustomerReponse>
    {
        private readonly ICustomerRepository _repo;
        public SearchCustomerQueryHandler(ICustomerRepository repo)
        {
            _repo = repo;
        }
        public async Task<SearchCustomerReponse> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
        {
            var res = await _repo.Search(request.CustomerCode,request.keyword,request.phone,request.isUnique,request.page, request.pagesize);
            return res;
        }
    }
}
