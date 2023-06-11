using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerSvc.Application.Persistences
{
    public interface IWorkerRepositories
    {
        Task<int>UpdateStatus(string Id, int Status);
    }
}
