using Blog.Application.Dtos.Categories;
using MediatR;

namespace Blog.Application.Commands.Categories.Create
{
    public class CreateCategoryCommand   :IRequest<CategoryDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public int? ParentId { get; set; }
        public bool ShowInHomePage { get; set; }
        public int DisplayOrder { get; set; }
    }
}