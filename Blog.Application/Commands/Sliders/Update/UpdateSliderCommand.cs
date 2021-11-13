using Blog.Application.Dtos.Categories;
using Blog.Application.Dtos.Sliders;
using MediatR;

namespace Blog.Application.Commands.Sliders.Update
{
    public class UpdateSliderCommand  :IRequest<SliderDto>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
    }
}