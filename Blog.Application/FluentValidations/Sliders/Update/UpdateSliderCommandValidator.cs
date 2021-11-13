using Blog.Application.Commands.Sliders.Update;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.Sliders.Update
{
    public class UpdateSliderCommandValidator  :AbstractValidator<UpdateSliderCommand>
    {
        public UpdateSliderCommandValidator()
        {
            RuleFor(c => c.Title).MaximumLength(1500).WithMessage(ValidationErrorResources.SliderTitleMaxLength)
                .NotEmpty().WithMessage(ValidationErrorResources.SliderTitleIsRequired);
            RuleFor(c => c.Description).NotEmpty().WithMessage(ValidationErrorResources.SliderDescriptionIsRequired);
            RuleFor(c => c.Image).NotEmpty().WithMessage(ValidationErrorResources.SliderImageIsRequired);
        }
    }
}
