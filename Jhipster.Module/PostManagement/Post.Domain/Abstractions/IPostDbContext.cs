using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities;
using System;
namespace Post.Domain.Abstractions
{
	public interface IPostDbContext
	{
        public DbSet<BoughtPost> BoughtPosts { get; set; }
        public DbSet<SalePost> SalePosts { get; set; }
        public DbSet<NewPost> NewPosts { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

