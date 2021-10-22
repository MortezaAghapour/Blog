using Blog.Domain.Entities.SocialNetworks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.FluentApiConfigurations.SocialNetworks
{
    public class SocialNetworkConfiguration :IEntityTypeConfiguration<SocialNetwork>
    {
        public void Configure(EntityTypeBuilder<SocialNetwork> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Icon).HasMaxLength(500);
            builder.Property(c => c.Color).HasMaxLength(50);
        }
    }
}