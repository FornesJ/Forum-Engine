using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentById(int id);
        Task<List<Comment>> GetAllComments();
        bool CreateComment(Comment comment, Post post, User user);
        User GetUser(int id);
        Post GetPost(int id);
        bool Save();
    }
}