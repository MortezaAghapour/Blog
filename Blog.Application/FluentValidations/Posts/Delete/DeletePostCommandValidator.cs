using Blog.Application.Commands.Posts.Delete;
using Blog.Shared.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.FluentValidations.Posts.Delete
{
    public class DeletePostCommandValidator:AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage(ValidationErrorResources.PostDeleteIdRequired)
              .LessThanOrEqualTo(0).WithMessage(ValidationErrorResources.PostIdNotLessThanZero);
        }
    }
}
