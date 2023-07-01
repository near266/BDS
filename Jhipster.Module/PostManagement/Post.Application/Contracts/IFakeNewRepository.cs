using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts
{
    public interface IFakeNewRepository
    {
        Task<string> ViewFakeNew(CancellationToken cancellationToken);
        Task<int> Update(Guid Id, string? Title, CancellationToken cancellationToken);
        Task<int> DeleteFakeNew(Guid Id, CancellationToken cancellationToken);
        Task<int> AddFakeNew(FakeNew rq, CancellationToken cancellationToken);
        Task<List<FakeNew>> ViewAllFake();
    }
}
