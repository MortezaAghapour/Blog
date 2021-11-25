using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dtos.Posts
{
    public class CreatePostDto
    {
        public long CategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string Author { get; set; }
        public bool IsPublish { get; set; }
        public List<string> Tags { get; set; }
        public IFormFile Image { get; set; }
    }
}
