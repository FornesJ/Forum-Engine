using ForumEngine.Data;
using ForumEngine.Interfaces;
using ForumEngine.Models;

namespace ForumEngine.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public Comment GetComment(int id)
        {
            return _context.Comments.Where(c => c.Id == id).FirstOrDefault();
        }

        public Comment GetComment(string title)
        {
            return _context.Comments.Where(c => c.Title == title).FirstOrDefault();
        }

        public Post GetPost(int id)
        {
            return _context.Comments.Where(c => c.Id == id).Select(p => p.Post).FirstOrDefault();
        }

        public User GetUser(int id)
        {
            return _context.Comments.Where(c => c.Id == id).Select(u => u.User).FirstOrDefault();
        }
    }
}