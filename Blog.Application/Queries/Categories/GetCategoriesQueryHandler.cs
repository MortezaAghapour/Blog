using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.Categories;
using Blog.Domain.Contracts.Repositories.Categories;
using Mapster;
using MediatR;

namespace Blog.Application.Queries.Categories
{
    public class GetSkillsQueryHandler : IRequestHandler<GetSkillsQuery, List<CategoryDto>>
    {
        #region Fields

        private readonly ICategoryRepository _categoryRepository;

        #endregion
        #region Constructors
        public GetSkillsQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion
        #region Methods
        public async Task<List<CategoryDto>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
        {
            var categories =await _categoryRepository.GetAll(asNoTracking: true, cancellationToken: cancellationToken);
            return categories.Adapt<List<CategoryDto>>();
        }
        #endregion

    }
}