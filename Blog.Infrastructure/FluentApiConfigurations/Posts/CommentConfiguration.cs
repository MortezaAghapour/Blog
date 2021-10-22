using Blog.Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.FluentApiConfigurations.Posts
{
    public class CommentConfiguration:IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.FullName).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(250).IsRequired();
            builder.Property(c => c.Description).IsRequired();
            builder.HasOne(c => c.Post).WithMany(c => c.Comments).HasForeignKey(c => c.PostId);
        }
    }
}