using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.Categories;
using Blog.Domain.Contracts.Repositories.Categories;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Domain.Entities.Categories;
using Blog.Shared.Exceptions;
using Mapster;
using MediatR;

namespace Blog.Application.Commands.Categories.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        #region Fields

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;


        #endregion
        #region Constructors
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Methods
        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetById(request.Id, cancellationToken);
            if (category is null)
            {
                throw new NotFoundException($"The Category Is Not Found In {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }

            // category = request.Adapt<Category>();//error occured => new instance error
            category.Name = request.Name;
            category.Icon = request.Icon;
            category.ParentId = request.ParentId;
            category.Description = request.Description;
            category.DisplayOrder = request.DisplayOrder;
            category.Image = request.Image;
            category.ShowInHomePage = category.ShowInHomePage;
            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return category.Adapt<CategoryDto>();
        }
        #endregion

    }
}