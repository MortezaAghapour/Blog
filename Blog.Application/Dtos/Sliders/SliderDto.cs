using Blog.Application.Dtos.Base;

namespace Blog.Application.Dtos.Sliders
{
    public class SliderDto:EntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
    }
}
