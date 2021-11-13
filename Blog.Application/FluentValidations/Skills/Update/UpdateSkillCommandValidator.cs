using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Commands.Skills.Update;
using Blog.Domain.Contracts.Repositories.Categories;
using Blog.Domain.Contracts.Repositories.Skills;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.Skills.Update
{
    public class UpdateSkillCommandValidator:AbstractValidator<UpdateSkillCommand>
    {
        #region Fields

        private readonly ISkillRepository _skillRepository;
        #endregion
        #region Constructors
        public UpdateSkillCommandValidator(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;

            RuleFor(c => c.Name).MaximumLength(500).WithMessage(ValidationErrorResources.SkillNameMaxLength)
                .NotEmpty().WithMessage(ValidationErrorResources.SkillNameIsRequired)
                .MustAsync(async (model, name, cancellation) => await CheckExistName(model.Id, name, cancellation)).WithMessage(ValidationErrorResources.SkillNameIsExist);
            RuleFor(c => c.Description).MaximumLength(1500).WithMessage(ValidationErrorResources.SkillDescriptionMaxLength);
        }

        #endregion
        #region Methods


        public async Task<bool> CheckExistName(long id, string name, CancellationToken cancellationToken = default)
        {
            return (await _skillRepository.Get(expression: c => c.Name.Trim().ToLower().Equals(name.Trim().ToLower()) && !c.Id.Equals(id), cancellationToken: cancellationToken)) is null;
        }
        #endregion
    }
}
