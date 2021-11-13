using Blog.Application.Dtos.Skills;
using MediatR;

namespace Blog.Application.Commands.Skills.Create
{
    public class CreateSkillCommand:IRequest<SkillDto>
    {
        public string Name { get; set; }
        public int Percentage { get; set; }
        public string Description { get; set; }
    }
}
