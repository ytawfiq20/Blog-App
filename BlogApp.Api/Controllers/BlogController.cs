using BlogApp.Api.Models;
using BlogApp.Data.Repositories.BlogRepository;
using BlogApp.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IRepository<Blog> _repository;
        public BlogController(IRepository<Blog> _repository)
        {
            this._repository = _repository;
        }

        [HttpGet("[action]")]
        public IActionResult GetAllBlogs()
        {
            try
            {
                var blogs = _repository.GetAll();
                if (blogs == null || blogs.ToList().Count==0)
                {
                    return NoContent();
                }
                return Ok(blogs);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response<Blog>
                        {
                            IsSuccess = false,
                            StatusCode = "500",
                            Message = "Unexpected error..."
                        });
            }

        }
        [HttpPost("[action]")]
        public IActionResult AddBlog([FromBody] Blog blog)
        {
            try
            {
                if (blog == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new Response<Blog>
                        {
                            IsSuccess = true,
                            StatusCode = "400",
                            Message = "Blog must't contain null values."
                        });
                }
                _repository.Add(blog);
                return StatusCode(StatusCodes.Status201Created,
                    new Response<Blog>
                    {
                        IsSuccess = true,
                        StatusCode = "201",
                        Message = "Blog added successfully.",
                        Tobject = new Blog { Id = blog.Id, Title = blog.Title, Description = blog.Description }
                    });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response<Blog>
                        {
                            IsSuccess = false,
                            StatusCode = "500",
                            Message = "Failde to add blog"
                        });
            }
        }
        [HttpGet("[action]/{id}")]
        public ActionResult<Blog> GetBlogById(Guid id)
        {
            try
            {
                Blog blog = _repository.GetById(id);
                bool isValid = Guid.NewGuid().ToString().Length == id.ToString().Length;
                if (blog == null || !isValid)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        new Response<Blog>
                        {
                            IsSuccess = true,
                            StatusCode = "404",
                            Message = "Invalid id."
                        });
                }
                return blog;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<Blog>
                    {
                        IsSuccess = false,
                        StatusCode = "500",
                        Message = $"Unexpected Error."
                    });
            }
        }
        [HttpPut("[action]")]
        public ActionResult<Blog> EditBlog([FromBody] Blog blog)
        {
            try
            {
                if (blog == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        new Response<Blog>
                        {
                            IsSuccess = true,
                            StatusCode = "404",
                            Message = "can't find this blog."
                        });
                }
                _repository.Edit(blog);
                return StatusCode(StatusCodes.Status200OK,
                    new Response<Blog>
                    {
                        IsSuccess = true,
                        StatusCode = "200",
                        Message = "Blog updated successfully.",
                        Tobject = new Blog { Id = blog.Id, Title = blog.Title, Description = blog.Description }
                    });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<Blog>
                    {
                        IsSuccess = false,
                        StatusCode = "500",
                        Message = $"Can't edit this blog."
                    });
            }
        }
        [HttpDelete("[action]/{id}")]
        public ActionResult<Blog> DeleteBlog(Guid id)
        {
            try
            {
                Blog blog = _repository.GetById(id);
                if (blog == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        new Response<Blog>
                        {
                            IsSuccess = true,
                            StatusCode = "404",
                            Message = "can't find this blog."
                        });
                }
                _repository.Delete(id);
                return StatusCode(StatusCodes.Status200OK,
                        new Response<Blog>
                        {
                            IsSuccess = true,
                            StatusCode = "200",
                            Message = "Blog deleted successfully.",
                            Tobject = new Blog { Id=blog.Id, Title= blog.Title, Description=blog.Description}
                        });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<Blog>
                    {
                        IsSuccess = false,
                        StatusCode = "500",
                        Message = $"Can't delete this blog.",
                    });
            }
        }
    }
}
