using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.Persistences
{
    public interface ITypeTermRepository
    {
        Task<IEnumerable<TypeTerm>> GetAll();
        Task<TypeTerm> GetById(Guid? Id);
        Task<int> AddTypeTerm(TypeTerm typeTerm, CancellationToken cancellationToken);
    }
}
