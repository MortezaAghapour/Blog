using Blog.Domain.Entities.Sliders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.FluentApiConfigurations.Sliders
{
    public class SliderConfiguration :IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(c => c.Title).HasMaxLength(1500).IsRequired();
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.Image).IsRequired();
        }
    }
}