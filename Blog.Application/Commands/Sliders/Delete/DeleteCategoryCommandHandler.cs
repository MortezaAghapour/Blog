using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Commands.Categories.Delete;
using Blog.Domain.Contracts.Repositories.Categories;
using Blog.Domain.Contracts.Repositories.Sliders;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Shared.Exceptions;
using MediatR;

namespace Blog.Application.Commands.Sliders.Delete
{
    public class DeleteSliderCommandHandler:IRequestHandler<DeleteSliderCommand,bool>
    {

        #region Fields
        private readonly ISliderRepository _sliderRepository;
        private readonly IUnitOfWork _unitOfWork;

       

        #endregion
        #region Constructors
                    public DeleteSliderCommandHandler(ISliderRepository sliderRepository, IUnitOfWork unitOfWork)
        {
            _sliderRepository = sliderRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Methods

        #endregion
        public async Task<bool> Handle(DeleteSliderCommand request, CancellationToken cancellationToken)
        {
            var slider = await _sliderRepository.GetById(request.Id, cancellationToken);
            if (slider is null)
            {
                throw new NotFoundException($"The Slider Is Not Found In {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }

            _sliderRepository.Delete(slider);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return true;
        }
    }
}
