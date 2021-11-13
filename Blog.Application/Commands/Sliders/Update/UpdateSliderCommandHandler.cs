using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Dtos.Categories;
using Blog.Application.Dtos.Sliders;
using Blog.Domain.Contracts.Repositories.Categories;
using Blog.Domain.Contracts.Repositories.Sliders;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Shared.Exceptions;
using Blog.Shared.Resources;
using FluentValidation;
using Mapster;
using MediatR;

namespace Blog.Application.Commands.Sliders.Update
{
    public class UpdateSliderCommandHandler : IRequestHandler<UpdateSliderCommand, SliderDto>
    {
        #region Fields

        private readonly ISliderRepository _sliderRepository;
        private readonly IUnitOfWork _unitOfWork;


        #endregion
        #region Constructors
        public UpdateSliderCommandHandler(ISliderRepository sliderRepository, IUnitOfWork unitOfWork)
        {
            _sliderRepository = sliderRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Methods
        public async Task<SliderDto> Handle(UpdateSliderCommand request, CancellationToken cancellationToken)
        {
            var slider = await _sliderRepository.GetById(request.Id, cancellationToken);
            if (slider is null)
            {
                throw new NotFoundException($"The Slider Not Found In {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }

            slider.Title = request.Title;
            slider.Description = request.Description;
            slider.Url = request.Url;
            if (!string.IsNullOrEmpty(slider.Image))
            { 
                slider.Image = request.Image;
            }
           
         
            _sliderRepository.Update(slider);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return slider.Adapt<SliderDto>();
        }
        #endregion

    }
}