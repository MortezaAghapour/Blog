using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Commands.Sliders.Create;
using Blog.Domain.Contracts.Repositories.Skills;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.Sliders.Create
{
    public class CreateSliderCommandValidator : AbstractValidator<CreateSliderCommand>
    {

        #region Constructors
        public CreateSliderCommandValidator()
        {    
            RuleFor(c => c.Title).MaximumLength(1500).WithMessage(ValidationErrorResources.SliderTitleMaxLength)
                .NotEmpty().WithMessage(ValidationErrorResources.SliderTitleIsRequired);
            RuleFor(c => c.Description).NotEmpty().WithMessage(ValidationErrorResources.SliderDescriptionIsRequired);
            RuleFor(c => c.Image).NotEmpty().WithMessage(ValidationErrorResources.SliderImageIsRequired);
        }
        #endregion



    }
}
