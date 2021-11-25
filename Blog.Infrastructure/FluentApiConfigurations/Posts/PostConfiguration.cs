using System.Collections.Generic;
using Blog.Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Blog.Infrastructure.FluentApiConfigurations.Posts
{
    public class PostConfiguration   :IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(c => c.Title).HasMaxLength(500).IsRequired();
            builder.Property(c => c.ShortDescription).HasMaxLength(1500).IsRequired();
            builder.Property(c => c.FullDescription).IsRequired();
            builder.Property(c => c.Author).HasMaxLength(250).IsRequired();
            builder.Property(c => c.Slug).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Tags).HasConversion(c => JsonConvert.SerializeObject(c),
                c => JsonConvert.DeserializeObject<List<string>>(c));
            builder.HasOne(c => c.Category).WithMany(c => c.Posts).HasForeignKey(c => c.CategoryId);

        }
    }
}