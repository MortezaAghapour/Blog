using Blog.Application.Commands.Sliders.Delete;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.Sliders.Delete
{
    public class DeleteSliderCommandValidator  :AbstractValidator<DeleteSliderCommand>
    {
        public DeleteSliderCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage(ValidationErrorResources.SliderDeleteIdRequired)
                .LessThanOrEqualTo(0).WithMessage(ValidationErrorResources.SliderIdNotLessThanZero);
        }
    }
}
