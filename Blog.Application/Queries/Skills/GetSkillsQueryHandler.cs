using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.Skills;
using Blog.Domain.Contracts.Repositories.Skills;
using Mapster;
using MediatR;

namespace Blog.Application.Queries.Skills
{
    public class GetSkillsQueryHandler : IRequestHandler<GetSkillsQuery, List<SkillDto>>
    {
        #region Fields

        private readonly ISkillRepository _skillRepository;

        #endregion
        #region Constructors
        public GetSkillsQueryHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        #endregion
        #region Methods
        public async Task<List<SkillDto>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills =await _skillRepository.GetAll(asNoTracking: true, cancellationToken: cancellationToken);
            return skills.Adapt<List<SkillDto>>();
        }
        #endregion

    }
}