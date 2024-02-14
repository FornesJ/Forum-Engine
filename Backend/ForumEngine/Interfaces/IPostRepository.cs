using ForumEngine.Models;

namespace ForumEngine.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetPostById(int id);
        Task<List<Post>> GetAllPosts();
        Task<Post> CreatePost(Post post, User user);
        Task<List<Comment>> GetComments(int id);
        Task<Post?> UpdatePost(int id, Post post);
        bool PostExists(int id);
        Task<Post?> DeletePost(Post post);
        bool DeletePosts(List<Post> posts);
        bool Save();
    }
}