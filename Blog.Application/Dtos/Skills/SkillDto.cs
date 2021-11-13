using Blog.Application.Dtos.Base;

namespace Blog.Application.Dtos.Skills
{
    public class SkillDto:EntityDto
    {
        public string Name { get; set; }
        public int Percentage { get; set; }
        public string Description { get; set; }
    }
}
