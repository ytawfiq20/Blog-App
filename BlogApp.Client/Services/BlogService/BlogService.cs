using BlogApp.Data.Entities;
using System.Net.Http.Json;

namespace BlogApp.Client.Services.BlogService
{
    public class BlogService : IBlog
    {
        private readonly HttpClient _httpClient;
        public BlogService(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }
        public async Task AddBlog(Blog blog)
        {
            await _httpClient.PostAsJsonAsync("Blog/AddBlog", blog);
        }

        public async Task DeleteBlog(Guid id)
        {
            await _httpClient.DeleteAsync($"Blog/DeleteBlog/{id}");
        }

        public async Task EditBlog(Blog blog)
        {
            await _httpClient.PutAsJsonAsync("Blog/EditBlog", blog);
        }

        public async Task<IEnumerable<Blog>> GetAllBlogs()
        {
            try
            {
                var response = await _httpClient.GetAsync("Blog/GetAllBlogs");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(IEnumerable<Blog>);
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<Blog>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code is: {response.StatusCode}, Message: {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Blog> GetBlogById(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Blog/GetBlogById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(Blog);
                    }
                    return await response.Content.ReadFromJsonAsync<Blog>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code is: {response.StatusCode}, Message: {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
