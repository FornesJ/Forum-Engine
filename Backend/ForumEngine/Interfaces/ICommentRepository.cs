using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentById(int id);
        Task<List<Comment>> GetAllComments();
        bool CreateComment(Comment comment, Post post, User user);
        bool UpdateComment(Comment comment);
        bool CommentExists(int id);
        bool Save();
    }
}