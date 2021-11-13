using Blog.Application.Commands.Categories.Delete;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.Categories.Delete
{
    public class DeleteCategoryCommandValidator :AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage(ValidationErrorResources.CategoryDeleteIdRequired)
                .LessThanOrEqualTo(0).WithMessage(ValidationErrorResources.CategoryIdNotLessThanZero);
        }
    }
}