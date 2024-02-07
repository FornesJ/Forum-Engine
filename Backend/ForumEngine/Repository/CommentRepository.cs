using ForumEngine.Data;
using ForumEngine.Interfaces;
using ForumEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumEngine.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CommentExists(int id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }

        public bool CreateComment(Comment comment, Post post, User user)
        {
            comment.Post = post;
            comment.PostId = post.Id;
            comment.User = user;
            comment.UserId = user.Id;
            _context.Comments.Add(comment);
            return Save();
        }

        public bool DeleteComment(Comment comment)
        {
            _context.Remove(comment);
            return Save();
        }

        public bool DeleteComments(List<Comment> comments)
        {
            _context.RemoveRange(comments);
            return Save();
        }

        public async Task<List<Comment>> GetAllComments()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateComment(Comment comment)
        {
            _context.Update(comment);
            return Save();
        }
    }
}