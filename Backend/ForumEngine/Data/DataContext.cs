using ForumEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumEngine.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments {get; set; }

        // configuring cascade delete
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
                .Entity<User>()
                .HasMany(e => e.Posts)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            modelBuilder
                .Entity<Post>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Post)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}