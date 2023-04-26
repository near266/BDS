using System;
using Microsoft.EntityFrameworkCore;
using Post.Domain.Abstractions;

namespace Post.Infrastructure.Persistences
{
    public class PostDbContext : DbContext, IPostDbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(true, cancellationToken);
        }

    }
}

