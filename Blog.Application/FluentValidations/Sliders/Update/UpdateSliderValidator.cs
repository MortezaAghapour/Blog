using Blog.Application.Dtos.Sliders;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.Sliders.Update
{
    public class UpdateSliderValidator :AbstractValidator<UpdateSliderDto>
    {
        public UpdateSliderValidator()
        {
            RuleFor(c => c.Title).MaximumLength(1500).WithMessage(ValidationErrorResources.SliderTitleMaxLength)
                .NotEmpty().WithMessage(ValidationErrorResources.SliderTitleIsRequired);
            RuleFor(c => c.Description).NotEmpty().WithMessage(ValidationErrorResources.SliderDescriptionIsRequired);
        }
    }
}