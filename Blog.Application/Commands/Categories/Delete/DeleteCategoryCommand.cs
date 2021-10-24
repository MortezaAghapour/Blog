using MediatR;

namespace Blog.Application.Commands.Categories.Delete
{
    public class DeleteCategoryCommand  :IRequest<bool>
    {
        public long Id { get; set; }
    }
}