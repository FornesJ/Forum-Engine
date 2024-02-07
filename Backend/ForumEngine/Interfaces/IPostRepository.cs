using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetPostById(int id);
        Task<List<Post>> GetAllPosts();
        bool CreatePost(Post post, User user);
        ICollection<Comment> GetComments(int id);
        bool UpdatePost(Post post);
        bool PostExists(int id);
        bool Save();
    }
}