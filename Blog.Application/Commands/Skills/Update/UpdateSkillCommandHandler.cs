using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.Skills;
using Blog.Domain.Contracts.Repositories.Skills;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Shared.Exceptions;
using Blog.Shared.Resources;
using FluentValidation;
using Mapster;
using MediatR;

namespace Blog.Application.Commands.Skills.Update
{
    public class UpdateSkillCommandHandler:IRequestHandler<UpdateSkillCommand,SkillDto>
    {
        #region Fields

        private readonly ISkillRepository _skillRepository;
        private readonly IUnitOfWork _unitOfWork;


        #endregion
        #region Constructors
        public UpdateSkillCommandHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Methods
        public async Task<SkillDto> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await _skillRepository.GetById(request.Id, cancellationToken);
            if (skill is null)
            {
                throw new NotFoundException($"The Skill Not Found In {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }

            var checkName = await _skillRepository.Get(
                expression: c => c.Name.Trim().ToLower().Equals(request.Name.Trim().ToLower()) && !c.Id.Equals(request.Id),
                cancellationToken: cancellationToken);
            if (!(checkName is null))
            {
                throw new ValidationException(ValidationErrorResources.TheCategoryNameIsDuplicate);
            }

            skill.Name = request.Name;
            skill.Description = request.Description;
            skill.Percentage = request.Percentage;
           
            _skillRepository.Update(skill);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return skill.Adapt<SkillDto>();
        }
        #endregion

    }
}
