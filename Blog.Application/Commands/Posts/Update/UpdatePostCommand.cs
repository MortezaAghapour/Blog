using Blog.Application.Dtos.Posts;
using Blog.Domain.Contracts.Repositories.Posts;
using Blog.Domain.Contracts.UnitOfWorks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Posts.Update
{
    public class UpdatePostCommand:IRequest<PostDto>
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; }
        public string ShortLink { get; set; }
        public bool IsPublish { get; set; }
        public DateTime PublishDate { get; set; }
      
    }
}
