using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface ICommentRepository
    {
        Comment GetComment(int id);
        Comment GetComment(string title);
        User GetUser(int id);
        Post GetPost(int id);
    }
}