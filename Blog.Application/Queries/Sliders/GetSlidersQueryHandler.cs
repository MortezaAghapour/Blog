using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.Sliders;
using Blog.Domain.Contracts.Repositories.Sliders;
using Mapster;
using MediatR;

namespace Blog.Application.Queries.Sliders
{
    public class GetSlidersQueryHandler : IRequestHandler<GetSlidersQuery, List<SliderDto>>
    {
        #region Fields

        private readonly ISliderRepository _sliderRepository;



        #endregion
        #region Costructors
        public GetSlidersQueryHandler(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        #endregion
        #region Methods
        public async Task<List<SliderDto>> Handle(GetSlidersQuery request, CancellationToken cancellationToken)
        {
            var sliders = await _sliderRepository.GetAll(asNoTracking: true, cancellationToken: cancellationToken);
            return sliders.Adapt<List<SliderDto>>();
        }
        #endregion

    }
}