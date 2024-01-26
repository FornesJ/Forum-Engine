using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        bool CreateUser(User user);
        Task<List<Post>> GetUserPosts(int id);
        Task<List<Comment>> GetUserComments(int id);
        bool Save();
    }
}