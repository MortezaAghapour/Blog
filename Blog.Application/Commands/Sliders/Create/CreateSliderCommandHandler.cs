using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.Sliders;
using Blog.Domain.Contracts.Repositories.Sliders;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Domain.Entities.Sliders;

using Mapster;
using MediatR;

namespace Blog.Application.Commands.Sliders.Create
{
    public class CreateSliderCommandHandler : IRequestHandler<CreateSliderCommand, SliderDto>
    {
        #region Fields
        private readonly ISliderRepository _sliderRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constrcutros
        public CreateSliderCommandHandler(ISliderRepository sliderRepository, IUnitOfWork unitOfWork)
        {
            _sliderRepository = sliderRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<SliderDto> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
        {
            var slider = request.Adapt<Slider>();
            await _sliderRepository.Insert(slider, cancellationToken);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return slider.Adapt<SliderDto>();
        }
        #endregion

    }
}