using Blog.Application.Dtos.Settings;
using Blog.Application.Dtos.Sliders;
using Blog.Domain.Contracts.Repositories.Posts;
using Blog.Domain.Contracts.Repositories.Settings;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Shared.Exceptions;
using Blog.Shared.Resources;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Settings.Update
{
    public class UpdateSettingCommandHandler : IRequestHandler<UpdateSettingCommand, SettingDto>
    {
        #region Fields
        private readonly ISettingRepository _settingRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion
        #region Constructors
        public UpdateSettingCommandHandler(ISettingRepository settingRepository, IUnitOfWork unitOfWork)
        {
            _settingRepository = settingRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion
        #region Methods
        public async Task<SettingDto> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
        {
            var setting = await _settingRepository.Get(
             expression: c => c.Id.Equals(request.Id),
             cancellationToken: cancellationToken);

            if (setting is null)
            {
                throw new NotFoundException($"The Setting Is Not Found In {GetType().Name} / {MethodBase.GetCurrentMethod().Name}");
            }
            setting.Value = request.Value;
            _settingRepository.Update(setting);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return setting.Adapt<SettingDto>();
        }
        #endregion

    }
}
