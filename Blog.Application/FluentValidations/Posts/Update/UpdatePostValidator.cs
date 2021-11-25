using Blog.Application.Dtos.Posts;
using Blog.Shared.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.FluentValidations.Posts.Update
{
    public class UpdatePostValidator:AbstractValidator<UpdatePostDto>
    {
        public UpdatePostValidator()
        {
            RuleFor(c => c.Title).MaximumLength(500).WithMessage(ValidationErrorResources.PostTitleMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostTitleIsRequired);
            RuleFor(c => c.ShortDescription).MaximumLength(1500).WithMessage(ValidationErrorResources.PostShortDescriptionMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostShorDescriptionIsRequired);
            RuleFor(c => c.FullDescription).NotEmpty().WithMessage(ValidationErrorResources.ThePostFullDescriptionIsRequired);
            RuleFor(c => c.Author).MaximumLength(250).WithMessage(ValidationErrorResources.PostAuthorMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostAuthorIsRequired);
            RuleFor(c => c.Slug).MaximumLength(500).WithMessage(ValidationErrorResources.PostSlugMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostSlugIsRequired);
            RuleFor(c => c.CategoryId).LessThanOrEqualTo(0).WithMessage(ValidationErrorResources.CategoryIdNotLessThanZero);
        }
    }
}
