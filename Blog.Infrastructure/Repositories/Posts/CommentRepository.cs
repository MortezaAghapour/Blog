using Blog.Domain.Contracts.Repositories.Posts;
using Blog.Domain.Entities.Posts;
using Blog.Infrastructure.ApplicationContexts;
using Blog.Infrastructure.Repositories.Base;

namespace Blog.Infrastructure.Repositories.Posts
{
    public class CommentRepository:Repository<Comment>,ICommentRepository
    {
        public CommentRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }
    }
}