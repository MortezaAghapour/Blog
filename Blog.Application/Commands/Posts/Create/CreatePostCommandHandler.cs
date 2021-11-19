using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.Posts;
using Blog.Domain.Contracts.Repositories.Posts;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Domain.Entities.Posts;
using Blog.Shared.Resources;
using FluentValidation;
using Mapster;
using MediatR;

namespace Blog.Application.Commands.Posts.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
    {
        #region Fields
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constructors
        public CreatePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Methods
        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.Get(
                expression: c => c.Slug.Trim().ToLower().Equals(request.Slug.Trim().ToLower()),
                cancellationToken: cancellationToken);

            if (!(post is null))
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
            post = request.Adapt<Post>();
            await _postRepository.Insert(post, cancellationToken);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return post.Adapt<PostDto>();
        }
        #endregion

    }
}