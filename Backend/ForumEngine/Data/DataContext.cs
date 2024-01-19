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
    }
}