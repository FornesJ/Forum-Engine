using ForumEngine.Data;
using ForumEngine.Interfaces;
using ForumEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumEngine.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public ICollection<Comment> GetComments(int id)
        {
            return _context.Comments.Where(u => u.UserId == id).ToList();
        }

        public ICollection<Post> GetUserPosts(int id)
        {
            return _context.Posts.Where(u => u.UserId == id).ToList();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}