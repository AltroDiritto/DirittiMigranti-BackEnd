using System.Linq;
using DirittoMigrantiAPI.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserAuth> UsersAuth { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consultant>().HasBaseType<User>();
            modelBuilder.Entity<Operator>().HasBaseType<User>();
        }
    }
}
