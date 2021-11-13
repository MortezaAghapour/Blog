using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Contracts.Repositories.Skills;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Shared.Exceptions;
using MediatR;

namespace Blog.Application.Commands.Skills.Delete
{
    public class DeleteSkillCommandHandler:IRequestHandler<DeleteSkillCommand,bool>
    {
        #region Fields
        private readonly ISkillRepository _skillRepository;
        private readonly IUnitOfWork _unitOfWork;



        #endregion
        #region Constructors
        public DeleteSkillCommandHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Methods

        #endregion
        public async Task<bool> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            var category = await _skillRepository.GetById(request.Id, cancellationToken);
            if (category is null)
            {
                throw new NotFoundException($"The Skill Is Not Found In {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }

            _skillRepository.Delete(category);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return true;
        }
    }
}
