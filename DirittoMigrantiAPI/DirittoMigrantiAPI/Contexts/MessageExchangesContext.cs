using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Models.Contexts
{
    //DbContext represents the connection to the database
    public class MessageExchangesContext : DbContext
    {
        public DbSet<MessageExchange> MessageExchanges { get; set; }

        public MessageExchangesContext(DbContextOptions<MessageExchangesContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One conv has many messages
            modelBuilder.Entity<Message>().HasOne(msg => msg.WhereIBelong).WithMany(conv => conv.Messages)
                .OnDelete(DeleteBehavior.SetNull).IsRequired();

            //One message has one writer 
            modelBuilder.Entity<Message>().HasOne(msg => msg.Author)
                .WithMany(u => u.Messages).OnDelete(DeleteBehavior.SetNull).IsRequired();
            
            //One conv has one owner 
            modelBuilder.Entity<MessageExchange>().HasOne(conv => conv.conversationOwner)
                .WithMany(op => op.MessageExchanges).OnDelete(DeleteBehavior.SetNull).IsRequired();
        }
    }

}
