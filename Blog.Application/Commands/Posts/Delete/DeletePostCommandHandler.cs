using Blog.Domain.Contracts.Repositories.Posts;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Shared.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Posts.Delete
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
    {
        #region Fields
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion
        #region Constructors
        public DeletePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Methods
        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetById(request.Id, cancellationToken);
            if (post == null)
            {
                throw new NotFoundException($"The Post Is Not Found In {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }
            _postRepository.Delete(post);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return true;
        }
        #endregion

    }
}
