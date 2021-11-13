using Blog.Application.Dtos.Sliders;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.Sliders.Create
{
    public class CreateSliderValidator :AbstractValidator<CreateSliderDto>
    {
        public CreateSliderValidator()
        {
            RuleFor(c => c.Title).MaximumLength(1500).WithMessage(ValidationErrorResources.SliderTitleMaxLength)
                .NotEmpty().WithMessage(ValidationErrorResources.SliderTitleIsRequired);
            RuleFor(c => c.Description).NotEmpty().WithMessage(ValidationErrorResources.SliderDescriptionIsRequired);
            RuleFor(c => c.Image).NotEmpty().WithMessage(ValidationErrorResources.SliderImageIsRequired);

        }
    }
}