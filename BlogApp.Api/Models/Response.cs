namespace BlogApp.Api.Models
{
    public class Response<T>
    {
        public string? StatusCode { get; set; }
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }

        public T? Tobject { get; set; }
    }
}
