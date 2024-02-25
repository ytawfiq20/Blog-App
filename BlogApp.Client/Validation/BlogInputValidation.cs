using BlogApp.Data.Entities;
using FluentValidation;

namespace BlogApp.Client.Validation
{
    public class BlogInputValidation : AbstractValidator<Blog>
    {
        public BlogInputValidation()
        {
            RuleFor(e => e.Title).NotEmpty().WithMessage("Blog title must not be empty.").NotNull();
            RuleFor(e => e.Description).NotEmpty().WithMessage("Blog description must not be empty.").NotNull();
        }
    }
}
