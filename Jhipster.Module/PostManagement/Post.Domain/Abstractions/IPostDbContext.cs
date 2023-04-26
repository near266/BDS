using System;
namespace Post.Domain.Abstractions
{
	public interface IPostDbContext
	{
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

