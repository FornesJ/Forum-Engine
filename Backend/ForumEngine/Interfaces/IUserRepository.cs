using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        bool CreateUser(User user);
        ICollection<Post> GetUserPosts(int id);
        ICollection<Comment> GetUserComments(int id);
        bool Save();
    }
}