using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetPostById(int id);
        Task<List<Post>> GetAllPosts();
        bool CreatePost(Post post, User user);
        User GetUser(int id);
        ICollection<Comment> GetComments(int id);
        bool Save();
    }
}