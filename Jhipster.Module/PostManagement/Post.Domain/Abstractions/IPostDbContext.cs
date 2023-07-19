using Jhipster.Domain;
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
        public DbSet<Notification> Notification { get; set; }
        public DbSet<FakeNew> FakeNew { get; set; }
        public DbSet<Comment> Comment { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

