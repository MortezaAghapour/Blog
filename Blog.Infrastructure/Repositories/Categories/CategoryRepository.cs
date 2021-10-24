using Blog.Domain.Contracts.Repositories.Categories;
using Blog.Domain.Entities.Categories;
using Blog.Infrastructure.ApplicationContexts;
using Blog.Infrastructure.Repositories.Base;

namespace Blog.Infrastructure.Repositories.Categories
{
    public class CategoryRepository :Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }
    }
}