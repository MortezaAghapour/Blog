using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Commands.Categories.Update;
using Blog.Domain.Contracts.Repositories.Categories;
using Blog.Shared.Resources;
using FluentValidation;

namespace Blog.Application.FluentValidations.Categories.Update
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        #region Fields

        private readonly ICategoryRepository _categoryRepository;
        #endregion
        #region Constructors
        public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(c => c.Name).MaximumLength(250).WithMessage(ValidationErrorResources.CategoryNameMaxLength)
                .NotEmpty().WithMessage(ValidationErrorResources.CategoryNameIsRequired)
                .MustAsync(async (model, name, cancellation) => await CheckExistName(model.Id, name, cancellation)).WithMessage(ValidationErrorResources.CategoryNameIsExist);
        }

        #endregion
        #region Methods


        public async Task<bool> CheckExistName(long id, string name, CancellationToken cancellationToken = default)
        {
            return (await _categoryRepository.Get(expression: c => c.Name.Trim().ToLower().Equals(name.Trim().ToLower()) && !c.Id.Equals(id), cancellationToken: cancellationToken)) is null;
        }
        #endregion
    }
}