using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Contracts.Repositories.Categories;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Shared.Exceptions;
using MediatR;

namespace Blog.Application.Commands.Categories.Delete
{
    public class DeleteCategoryCommandHandler:IRequestHandler<DeleteCategoryCommand,bool>
    {

        #region Fields
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

       

        #endregion
        #region Constructors
                    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Methods

        #endregion
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetById(request.Id, cancellationToken);
            if (category is null)
            {
                throw new NotFoundException($"The Category Is Not Found In {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }

            _categoryRepository.Delete(category);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return true;
        }
    }
}
