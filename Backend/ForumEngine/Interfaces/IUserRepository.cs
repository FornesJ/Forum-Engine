using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User GetUser(string firstName, string lastName);
        ICollection<Post> GetUserPosts(int id);
        ICollection<Comment> GetComments(int id);
    }
}