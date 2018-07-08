using DirittoMigrantiAPI.Models;
using DirittoMigrantiAPI.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DirittoMigrantiAPI.Contexts
{
    public class MyAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserAuth> UsersAuth { get; set; }
        public DbSet<MessageExchange> MessageExchanges { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Content> Contents { get; set; }
        
        public MyAppContext(DbContextOptions<MyAppContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Users
            modelBuilder.Entity<Consultant>().HasBaseType<User>();
            modelBuilder.Entity<Operator>().HasBaseType<User>();
            #endregion

            #region Conversation

            //One message has one writer 
            modelBuilder.Entity<Message>().HasOne(msg => msg.Author)
                .WithMany(u => u.Messages).OnDelete(DeleteBehavior.SetNull).IsRequired();

            //One conv has one owner 
            modelBuilder.Entity<MessageExchange>().HasOne(conv => conv.conversationOwner)
                .WithMany(op => op.MessageExchanges).OnDelete(DeleteBehavior.SetNull).IsRequired();

            //One conv has many messages
            modelBuilder.Entity<MessageExchange>().HasMany(conv=>conv.Messages).WithOne(msg=>msg.BelongingConv)
                .OnDelete(DeleteBehavior.SetNull).IsRequired();

            #endregion

            #region Content
            modelBuilder.Entity<News>().HasBaseType<Content>();
            modelBuilder.Entity<Practice>().HasBaseType<Content>();

            modelBuilder.Entity<News>().HasOne(n => n.Writer).WithMany(u => u.WrittenNews).OnDelete(DeleteBehavior.SetNull).IsRequired();
            modelBuilder.Entity<Practice>().HasOne(c => c.Writer).WithMany(u => u.WrittenPractices).OnDelete(DeleteBehavior.SetNull).IsRequired();
            #endregion
        }

    }
}
