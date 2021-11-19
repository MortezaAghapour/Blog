using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Contracts.Repositories.SocialNetworks;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Shared.Exceptions;
using MediatR;

namespace Blog.Application.Commands.SocialNetworks.Delete
{
    public class DeleteSocialNetworkCommandHandler : IRequestHandler<DeleteSocialNetworkCommand, bool>
    {
        #region Fields

        private readonly ISocialNetworkRepository _socialNetworkRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constrcutors
        public DeleteSocialNetworkCommandHandler(ISocialNetworkRepository socialNetworkRepository, IUnitOfWork unitOfWork)
        {
            _socialNetworkRepository = socialNetworkRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Methods
        public async Task<bool> Handle(DeleteSocialNetworkCommand request, CancellationToken cancellationToken)
        {
            var socialNetwork = await _socialNetworkRepository.GetById(request.Id, cancellationToken);
            if (socialNetwork is null)
            {
                throw new NotFoundException($"The SocialNetwork Is Not Found In {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }
            _socialNetworkRepository.Delete(socialNetwork);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return true;
        }
        #endregion

    }
}