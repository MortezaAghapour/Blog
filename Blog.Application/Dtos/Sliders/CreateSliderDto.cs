using Microsoft.AspNetCore.Http;

namespace Blog.Application.Dtos.Sliders
{
    public class CreateSliderDto
    {
      
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public IFormFile  Image { get; set; }
    }
}