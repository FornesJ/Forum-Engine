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

        public async Task<Post> CreatePost(Post post, User user)
        {
            post.User = user;
            post.UserId = user.Id;
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> DeletePost(Post post)
        {
            if (post == null)
            {
                return null;
            }

            _context.Remove(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public bool DeletePosts(List<Post> posts)
        {
            _context.RemoveRange(posts);
            return Save();
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<List<Comment>> GetComments(int id)
        {
            var comments = await _context.Comments.ToListAsync();
            
            return comments.Where(p => p.PostId == id).ToList();
        }

        public async Task<Post?> GetPostById(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public bool PostExists(int id)
        {
            return _context.Posts.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<Post?> UpdatePost(int id, Post post)
        {
            var existingPost = await _context.Posts.FindAsync(id);

            if (existingPost == null)
            {
                return null;
            }
            
            existingPost.Title = post.Title;
            existingPost.Content = post.Content;

            await _context.SaveChangesAsync();

            return existingPost;
        }
    }
}