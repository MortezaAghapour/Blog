using Blog.Application.Dtos.Skills;
using MediatR;

namespace Blog.Application.Commands.Skills.Update
{
    public class UpdateSkillCommand:IRequest<SkillDto>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }
        public string Description { get; set; }
    }
}
