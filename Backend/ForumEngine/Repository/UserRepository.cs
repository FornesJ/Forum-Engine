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

        public async Task<List<Comment>> GetUserComments(int id)
        {
            var comments = await _context.Comments.ToListAsync();
            return comments.Where(c => c.UserId == id).ToList();
        }

        public async Task<List<Post>> GetUserPosts(int id)
        {
            var posts = await _context.Posts.ToListAsync();
            return posts.Where(p => p.UserId == id).ToList();
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

        public async Task<User?> UpdateUser(int id, User user)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public async Task<User?> DeleteUser(User user)
        {
            if (user == null)
            {
                return null;
            }

            _context.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}