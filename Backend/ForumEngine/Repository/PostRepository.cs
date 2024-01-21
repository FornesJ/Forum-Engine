using ForumEngine.Data;
using ForumEngine.Interfaces;
using ForumEngine.Models;

namespace Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        
        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Comment> GetComments(int id)
        {
            return _context.Comments.Where(p => p.PostId == id).ToList();
        }

        public Post GetPost(int id)
        {
            return _context.Posts.Where(p => p.Id == id).FirstOrDefault();
        }

        public Post GetPost(string title)
        {
            return _context.Posts.Where(p => p.Title == title).FirstOrDefault();
        }

        public User GetUser(int id)
        {
            return _context.Posts.Where(p => p.Id == id).Select(u => u.User).FirstOrDefault();
        }
    }
}