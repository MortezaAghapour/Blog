using System.Collections.Generic;
using Blog.Application.Dtos.Sliders;
using MediatR;

namespace Blog.Application.Queries.Sliders
{
    public class GetSliderQuery  :IRequest<List<SliderDto>>
    {
        
    }
}