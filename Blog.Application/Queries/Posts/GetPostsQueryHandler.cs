using Blog.Application.Dtos.Posts;
using Blog.Domain.Contracts.Repositories.Posts;
using Blog.Domain.Entities.Posts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Posts
{
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<PostDto>>
    {
        #region Fields
        private readonly IPostRepository _postRepository;
        #endregion
        #region Constructors
        public GetPostsQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        #endregion
        #region Methods
        public async Task<List<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var posts =await _postRepository.GetAll(cancellationToken: cancellationToken,include:c=>c.Include(d=>d.Category));
            //_ = TypeAdapterConfig<Post, PostDto>.NewConfig()
            //         .Map(d => d.CategoryName, s => s.Category.Name);
            var config = new TypeAdapterConfig();
            config.ForType<Post,PostDto>().Map(d => d.CategoryName, s => s.Category.Name);
            return posts.Adapt<List<PostDto>>(config);
        }
        #endregion

    }
}
