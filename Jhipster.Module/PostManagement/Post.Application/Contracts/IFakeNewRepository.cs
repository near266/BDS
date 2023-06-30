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
    }
}
