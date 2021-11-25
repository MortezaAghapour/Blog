using Blog.Application.Commands.Posts.Update;
using Blog.Domain.Contracts.Repositories.Posts;
using Blog.Shared.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.FluentValidations.Posts.Update
{
    public class UpdatePostCommandValidator:AbstractValidator<UpdatePostCommand>
    {
        #region Fields
        private readonly IPostRepository _postRepository;

      
        #endregion
        #region Constructors
  public UpdatePostCommandValidator(IPostRepository postRepository)
        {
            _postRepository = postRepository;
            RuleFor(c => c.CategoryId).LessThanOrEqualTo(0).WithMessage(ValidationErrorResources.CategoryIdNotLessThanZero);
            RuleFor(c => c.Title).MaximumLength(500).WithMessage(ValidationErrorResources.PostTitleMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostTitleIsRequired);
            RuleFor(c => c.ShortDescription).MaximumLength(1500).WithMessage(ValidationErrorResources.PostShortDescriptionMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostShorDescriptionIsRequired);
            RuleFor(c => c.FullDescription).NotEmpty().WithMessage(ValidationErrorResources.ThePostFullDescriptionIsRequired);
            RuleFor(c => c.Author).MaximumLength(250).WithMessage(ValidationErrorResources.PostAuthorMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostAuthorIsRequired);
            RuleFor(c => c.Slug).MaximumLength(500).WithMessage(ValidationErrorResources.PostSlugMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostSlugIsRequired).MustAsync(async (model, slug, cancellation) => await CheckSlug(model.Id, slug, cancellation)).WithMessage(ValidationErrorResources.ThePostSlugIsDuplicate);

        }
        #endregion
        #region Methods
        public async Task<bool> CheckSlug(long id, string slug, CancellationToken cancellationToken = default)
        {
            return (await _postRepository.Get(expression: c => c.Slug.Trim().ToLower().Equals(slug.Trim().ToLower()) && !c.Id.Equals(id), cancellationToken: cancellationToken)) is null;
        }
        #endregion
    }
}
