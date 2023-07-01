using AutoMapper;
using Jhipster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerSvc.Application.Persistences;

namespace WorkerSvc.Infrastructure.Repositories
{
    public class WorkerRepositories : IWorkerRepositories
    {
        private readonly ApplicationDatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        public WorkerRepositories(ApplicationDatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }
        public async Task<int> UpdateStatus(string Id, int Status)
        {
            var check = await _databaseContext.SalePosts.FirstOrDefaultAsync(i => i.Id == Id);
            check.Status = Status;
            return await _databaseContext.SaveChangesAsync();
        }

        public async Task<int> UpdateOrderFakeNew(Guid Id)
        {
            var check = await _databaseContext.FakeNew.FirstOrDefaultAsync(i => i.Id == Id);
            check.Order = 0;
            check.OrderMax = 0;
            check.CreatedDate = DateTime.Now;
            return await _databaseContext.SaveChangesAsync();
        }
    }
}
