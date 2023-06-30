using System;
using Jhipster.Domain;
using Microsoft.EntityFrameworkCore;
using Post.Domain.Abstractions;
using Post.Domain.Entities;

namespace Post.Infrastructure.Persistences
{
    public class PostDbContext : DbContext, IPostDbContext
    {
        public DbSet<BoughtPost> BoughtPosts { get; set; }
        public DbSet<SalePost> SalePosts { get; set; }

        public DbSet<NewPost> NewPosts { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<FakeNew> FakeNew { get; set; }
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

