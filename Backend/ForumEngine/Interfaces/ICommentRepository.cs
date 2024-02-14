using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment?> GetCommentById(int id);
        Task<List<Comment>> GetAllComments();
        Task<Comment> CreateComment(Comment comment, Post post, User user);
        Task<Comment?> UpdateComment(int id, Comment comment);
        bool CommentExists(int id);
        Task<Comment?> DeleteComment(Comment comment);
        bool DeleteComments(List<Comment> comments);
        bool Save();
    }
}