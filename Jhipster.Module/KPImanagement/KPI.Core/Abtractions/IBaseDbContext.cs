using System;
namespace KPI.Core.Abtractions
{
    public interface IBaseDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

