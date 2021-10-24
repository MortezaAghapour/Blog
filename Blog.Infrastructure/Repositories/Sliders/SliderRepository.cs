using Blog.Domain.Contracts.Repositories.Sliders;
using Blog.Domain.Entities.Sliders;
using Blog.Infrastructure.ApplicationContexts;
using Blog.Infrastructure.Repositories.Base;

namespace Blog.Infrastructure.Repositories.Sliders
{
    public class SliderRepository  :Repository<Slider>,ISliderRepository
    {
        public SliderRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }
    }
}