using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        Task<User> CreateUser(User user);
        Task<List<Post>> GetUserPosts(int id);
        Task<List<Comment>> GetUserComments(int id);
        Task<User?> UpdateUser(int id, User user);
        bool UserExists(int id);
        Task<User?> DeleteUser(User user);
        bool Save();
    }
}