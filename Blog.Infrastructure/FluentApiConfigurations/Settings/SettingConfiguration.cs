using Blog.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.FluentApiConfigurations.Settings
{
    public class SettingConfiguration   :IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(c => c.Key).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Value).IsRequired();
        }
    }
}