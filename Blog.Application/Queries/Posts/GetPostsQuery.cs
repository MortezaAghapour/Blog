using Blog.Application.Dtos.Posts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts
{
    public class GetPostsQuery:IRequest<List<PostDto>>
    {
    }
}
