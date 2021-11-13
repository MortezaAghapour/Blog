using MediatR;

namespace Blog.Application.Commands.Sliders.Delete
{
    public class DeleteSliderCommand  :IRequest<bool>
    {
        public long Id { get; set; }
    }
}