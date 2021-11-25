using Blog.Application.Dtos.Posts;
using Blog.Domain.Contracts.Repositories.Posts;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Shared.Exceptions;
using Blog.Shared.Resources;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Posts.Update
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, PostDto>
    {
        #region Fields
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion
        #region Constructors
        public UpdatePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Methods
        public async Task<PostDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.Get(
              expression: c=>c.Id.Equals(request.Id),
              cancellationToken: cancellationToken);

            if (post is null)
            {
                throw new NotFoundException($"The Post Is Not Found In {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }
            if (post.Slug.Trim().ToLower().Equals(request.Slug.Trim().ToLower()))
            {
                throw new ValidationException(ValidationErrorResources.ThePostSlugIsDuplicate);
            }
            if (request.CategoryId <= 0)
            {
                throw new ValidationException(ValidationErrorResources.CategoryIdNotLessThanZero);
            }

            if (!string.IsNullOrEmpty(request.Image))
            {
                throw new ValidationException(ValidationErrorResources.PostImageIsRequired);
            }
            post.Slug= request.Slug;
            post.Tags = request.Tags;
            post.ShortLink = request.ShortLink;
            post.IsPublish = request.IsPublish;
            post.Title = request.Title;
            post.CategoryId = request.CategoryId;
            post.Author=request.Author;
            post.ShortDescription = request.ShortDescription;
            post.FullDescription = request.FullDescription;

            _postRepository.Update(post);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return post.Adapt<PostDto>();
        }
        #endregion

    }
}
