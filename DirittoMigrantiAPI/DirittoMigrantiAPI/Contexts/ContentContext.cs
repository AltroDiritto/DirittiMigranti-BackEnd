using System;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Models.Contexts
{
    public class ContentContext : DbContext
    {
        public ContentContext(DbContextOptions<ContentContext> options)
            : base(options)
        {
        }

        public DbSet<TextContent> Contents { get; set; }
    }
}
