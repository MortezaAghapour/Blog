using MediatR;

namespace Blog.Application.Commands.Skills.Delete
{
    public class DeleteSkillCommand:IRequest<bool>
    {
        public long Id { get; set; }
    }
}
