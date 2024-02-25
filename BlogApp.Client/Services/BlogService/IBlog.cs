using BlogApp.Data.Entities;

namespace BlogApp.Client.Services.BlogService
{
    public interface IBlog
    {
        Task AddBlog(Blog blog);
        Task EditBlog(Blog blog);
        Task DeleteBlog(Guid id);
        Task<Blog> GetBlogById(Guid id);
        Task<IEnumerable<Blog>> GetAllBlogs();
    }
}
