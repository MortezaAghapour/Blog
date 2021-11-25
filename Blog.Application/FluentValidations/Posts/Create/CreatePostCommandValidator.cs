using Blog.Application.Commands.Posts.Create;
using Blog.Domain.Contracts.Repositories.Posts;
using Blog.Shared.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.FluentValidations.Posts.Create
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        #region Fields
        private readonly IPostRepository _postRepository;


        #endregion
        #region Constructors
        public CreatePostCommandValidator(IPostRepository postRepository)
        {
            _postRepository = postRepository;
            RuleFor(c => c.CategoryId).LessThanOrEqualTo(0).WithMessage(ValidationErrorResources.CategoryIdNotLessThanZero);
            RuleFor(c => c.Title).MaximumLength(500).WithMessage(ValidationErrorResources.PostTitleMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostTitleIsRequired);
            RuleFor(c => c.ShortDescription).MaximumLength(1500).WithMessage(ValidationErrorResources.PostShortDescriptionMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostShorDescriptionIsRequired);
            RuleFor(c => c.FullDescription).NotEmpty().WithMessage(ValidationErrorResources.ThePostFullDescriptionIsRequired);
            RuleFor(c => c.Author).MaximumLength(250).WithMessage(ValidationErrorResources.PostAuthorMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostAuthorIsRequired);
            RuleFor(c => c.Slug).MaximumLength(500).WithMessage(ValidationErrorResources.PostSlugMaxLength).NotEmpty().WithMessage(ValidationErrorResources.ThePostSlugIsRequired).MustAsync(CheckExistSlug).WithMessage(ValidationErrorResources.ThePostSlugIsDuplicate);
        }
        #endregion
        #region Methods
        public async Task<bool> CheckExistSlug(string slug, CancellationToken cancellationToken)
        {
            return (await _postRepository.Get(expression: c => c.Slug.Trim().ToLower().Equals(slug.Trim().ToLower()), cancellationToken: cancellationToken)) is null;
        }
        #endregion
    }
}
