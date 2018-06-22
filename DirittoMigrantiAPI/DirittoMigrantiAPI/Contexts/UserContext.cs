using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Models
{
    //DbContext represents the connection to the database
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
