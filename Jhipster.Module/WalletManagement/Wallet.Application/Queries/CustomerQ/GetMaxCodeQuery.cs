using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;

namespace Wallet.Application.Queries.CustomerQ
{
    public class GetMaxCodeQuery : IRequest<string>
    {

    }
    public class GetMaxCodeQueryHandler : IRequestHandler<GetMaxCodeQuery, string>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetMaxCodeQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<string> Handle(GetMaxCodeQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetMaxCode();
        }
    }   
}
