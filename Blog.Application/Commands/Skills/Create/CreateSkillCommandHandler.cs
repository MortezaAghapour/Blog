using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.Skills;
using Blog.Domain.Contracts.Repositories.Skills;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Domain.Entities.Skills;
using Blog.Shared.Resources;
using FluentValidation;
using Mapster;
using MediatR;

namespace Blog.Application.Commands.Skills.Create
{
    public class CreateSkillCommandHandler:IRequestHandler<CreateSkillCommand,SkillDto>
    {

        #region Fields

        private readonly ISkillRepository _skillRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constructors

        public CreateSkillCommandHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion
        #region Methods
        public async Task<SkillDto> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill =await _skillRepository.Get(expression: c => c.Name.Trim().ToLower().Equals(request.Name.Trim().ToLower()),
                cancellationToken: cancellationToken);
            if (skill is null)
            {
                throw new ValidationException(ValidationErrorResources.TheSkillNameIsDuplicate);
            }

            skill = request.Adapt<Skill>();
            await _skillRepository.Insert(skill,cancellationToken);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return skill.Adapt<SkillDto>();

        }

        #endregion
    }
}
