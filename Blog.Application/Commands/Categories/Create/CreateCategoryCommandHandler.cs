using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.Categories;
using Blog.Domain.Contracts.Repositories.Categories;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Domain.Entities.Categories;
using Mapster;
using MediatR;

namespace Blog.Application.Commands.Categories.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        #region Fields
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constrcutros
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            
           
             var category = request.Adapt<Category>();
             category.ParentId = category.ParentId == 0 ? null : category.ParentId;
            await _categoryRepository.Insert(category, cancellationToken);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return category.Adapt<CategoryDto>();
        }
        #endregion

    }
}