using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Commands.Categories.Create;
using Blog.Domain.Contracts.Repositories.Categories;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.Categories.Create
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        #region Fields

        private readonly ICategoryRepository _categoryRepository;
        #endregion
        #region Constructors
        public CreateCategoryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(c => c.Name).MaximumLength(250).WithMessage(ValidationErrorResources.CategoryNameMaxLength)
            .NotEmpty().WithMessage(ValidationErrorResources.CategoryNameIsRequired)
            .MustAsync(CheckExistName).WithMessage(ValidationErrorResources.CategoryNameIsExist);  
        }
        #endregion
        #region Methods


        public async Task<bool> CheckExistName(string name, CancellationToken cancellationToken)
        {
            return (await _categoryRepository.Get(expression: c => c.Name.Trim().ToLower().Equals(name.Trim().ToLower()), cancellationToken: cancellationToken)) is null;
        }
        #endregion

    }
}
