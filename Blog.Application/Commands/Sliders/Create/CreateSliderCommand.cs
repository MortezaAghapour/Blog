using Blog.Application.Dtos.Sliders;
using MediatR;

namespace Blog.Application.Commands.Sliders.Create
{
    public class CreateSliderCommand   :IRequest<SliderDto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
    }
}