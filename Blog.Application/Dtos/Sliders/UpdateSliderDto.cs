using Blog.Application.Dtos.Base;
using Microsoft.AspNetCore.Http;

namespace Blog.Application.Dtos.Sliders
{
    public class UpdateSliderDto:EntityDto
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public IFormFile Image { get; set; }
    }
}