using Blog.Application.Commands.Skills.Delete;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.Skills.Delete
{
    public class DeleteSkillCommandValidator  :AbstractValidator<DeleteSkillCommand>
    {
        public DeleteSkillCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage(ValidationErrorResources.SkillDeleteIdRequired)
                .LessThanOrEqualTo(0).WithMessage(ValidationErrorResources.SkillIdNotLessThanZero);
        }
    }
}
