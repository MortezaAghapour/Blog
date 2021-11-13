using System.Collections.Generic;
using Blog.Application.Dtos.Categories;
using MediatR;

namespace Blog.Application.Queries.Categories
{
    public class GetSkillsQuery :IRequest<List<CategoryDto>>
    {
        
    }
}