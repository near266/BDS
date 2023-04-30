using Jhipster.Crosscutting.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.Persistences
{
    public interface ICustomerRepository
    {
        Task<int> Add(Customer cus,CancellationToken cancellationToken);
        Task<int> Update(Customer cus, CancellationToken cancellationToken);
        Task<int> Delete(Guid Id,CancellationToken cancellationToken);
        Task<Customer> GetById (Guid Id);
        Task<PagedList<Customer>> Search(int page, int pagesize);
    }
}
