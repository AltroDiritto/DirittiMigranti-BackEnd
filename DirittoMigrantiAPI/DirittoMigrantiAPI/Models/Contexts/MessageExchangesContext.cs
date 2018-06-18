using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Models.Contexts
{
        //DbContext represents the connection to the database
        public class MessageExchangesContext : DbContext
        {
            public MessageExchangesContext(DbContextOptions<MessageExchangesContext> options)
                : base(options)
            {
            }

        public DbSet<MessageExchange> MessageExchanges { get; set; }
        }

}
