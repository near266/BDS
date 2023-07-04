using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Post.Domain.Entities;
using System.Threading.Tasks;

namespace WorkerSvc.Application.Persistences
{
    public interface IWorkerRepositories
    {
        Task<int> UpdateStatus(string Id, int Status);
        Task<int> UpdateOrderFakeNew(Guid Id);
        Task<int> RepostSalePost(string? postId, int type, CancellationToken cancellationToken);
    }
}
