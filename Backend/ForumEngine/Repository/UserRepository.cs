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

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public ICollection<Comment> GetUserComments(int id)
        {
            return _context.Comments.Where(c => c.UserId == id).ToList();
        }

        public ICollection<Post> GetUserPosts(int id)
        {
            return _context.Posts.Where(p => p.UserId == id).ToList();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }
    }
}