using Blog.Domain.Entities.Skills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.FluentApiConfigurations.Skills
{
    public class SkillConfiguration :IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(1500);
        }
    }
}