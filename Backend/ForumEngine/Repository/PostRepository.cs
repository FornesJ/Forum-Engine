using ForumEngine.Data;
using ForumEngine.Interfaces;
using ForumEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        
        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePost(Post post, User user)
        {
            post.User = user;
            post.UserId = user.Id;
            _context.Posts.Add(post);
            return Save();
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public ICollection<Comment> GetComments(int id)
        {
            return _context.Comments.Where(p => p.PostId == id).ToList();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}