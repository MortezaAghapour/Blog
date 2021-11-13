using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Commands.Skills.Create;
using Blog.Domain.Contracts.Repositories.Skills;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.Skills.Create
{
    public class CreateSliderCommandValidator:AbstractValidator<CreateSkillCommand>
    {
      
        #region Fields

        private readonly ISkillRepository _skillRepository;
        #endregion
        #region Constructors
        public CreateSliderCommandValidator(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;

            RuleFor(c => c.Name).MaximumLength(250).WithMessage(ValidationErrorResources.SkillNameMaxLength)
                .NotEmpty().WithMessage(ValidationErrorResources.SkillNameIsRequired)
                .MustAsync(CheckExistName).WithMessage(ValidationErrorResources.TheSkillNameIsDuplicate);
            RuleFor(c => c.Description).MaximumLength(1500).WithMessage(ValidationErrorResources.SkillDescriptionMaxLength);
        }
        #endregion
        #region Methods


        public async Task<bool> CheckExistName(string name, CancellationToken cancellationToken)
        {
            return (await _skillRepository.Get(expression: c => c.Name.Trim().ToLower().Equals(name.Trim().ToLower()), cancellationToken: cancellationToken)) is null;
        }
        #endregion

    
}
}
