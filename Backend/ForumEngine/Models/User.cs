namespace ForumEngine.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Post> Posts { get; set;}
        public ICollection<Comment> Comments { get; set; }
    }
}
