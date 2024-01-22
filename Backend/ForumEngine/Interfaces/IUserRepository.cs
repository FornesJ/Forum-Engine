using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        Task<User> CreateUser(User user);
        ICollection<Post> GetUserPosts(int id);
        ICollection<Comment> GetComments(int id);
    }
}