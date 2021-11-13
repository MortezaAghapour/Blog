using System.Collections.Generic;
using Blog.Application.Dtos.Skills;
using MediatR;

namespace Blog.Application.Queries.Skills
{
    public class GetSkillsQuery :IRequest<List<SkillDto>>
    {
        
    }
}