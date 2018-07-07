using System;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Models.Contexts
{
    public class ContentContext : DbContext
    {
        public DbSet<Content> Contents { get; set; }

        public ContentContext(DbContextOptions<ContentContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().HasBaseType<Content>();
            modelBuilder.Entity<Practice>().HasBaseType<Content>();

            modelBuilder.Entity<News>().HasOne(c => c.Writer).WithMany(u=>u.WrittenNews).OnDelete(DeleteBehavior.SetNull).IsRequired();
            modelBuilder.Entity<Practice>().HasOne(c => c.Writer).WithMany(u => u.WrittenPractices).OnDelete(DeleteBehavior.SetNull).IsRequired();
        }
    }
}
