using ForumEngine.Data;
using ForumEngine.Interfaces;
using ForumEngine.Models;

namespace ForumEngine.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Comment> GetComments()
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUser(string firstName, string lastName)
        {
            return _context.Users.Where(u => u.FirstName == firstName && u.LastName == lastName).FirstOrDefault();
        }

        public ICollection<Post> GetUserPosts()
        {
            throw new NotImplementedException();
        }
    }
}