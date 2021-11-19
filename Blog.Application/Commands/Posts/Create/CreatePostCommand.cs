using System;
using System.Collections.Generic;
using Blog.Application.Dtos.Posts;
using MediatR;

namespace Blog.Application.Commands.Posts.Create
{
    public class CreatePostCommand   :IRequest<PostDto>
    {
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