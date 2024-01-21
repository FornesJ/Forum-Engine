using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface IPostRepository
    {
        Post GetPost(int id);
        Post GetPost(string title);
        User GetUser(int id);
        ICollection<Comment> GetComments();
    }
}