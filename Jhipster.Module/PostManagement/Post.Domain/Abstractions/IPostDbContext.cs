using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities;
using System;
namespace Post.Domain.Abstractions
{
	public interface IPostDbContext
	{
        public DbSet<BoughtPost> BoughtPosts { get; set; }
        public DbSet<SalePost> SalePosts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

