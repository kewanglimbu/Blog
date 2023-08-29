using BlogWebApp.Models;

namespace BlogWebApp.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        void AddPost(Post post);    
        void UpdatePost(Post post);
        void DeletePost(int id);

        Task<bool> SaveChangesAsync();
        
    }
}
