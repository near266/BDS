using Jhipster.Crosscutting.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Domain.Entities;

namespace Wallet.Application.Persistences
{
    public interface ICustomerRepository
    {
        Task<int> Add(Customer cus,CancellationToken cancellationToken);
        Task<int> Update(Customer cus, CancellationToken cancellationToken);
        Task<int> Delete(List<Guid> Id,CancellationToken cancellationToken);
        Task<DetailCusDTO> GetById (Guid Id);
        Task<SearchCustomerReponse> Search(string? keyword, string? phone, bool? isUnique, int page, int pagesize);
    }
}
