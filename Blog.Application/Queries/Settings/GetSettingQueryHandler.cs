using Blog.Application.Dtos.Settings;
using Blog.Domain.Contracts.Repositories.Settings;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Settings
{
    public class GetSettingQueryHandler : IRequestHandler<GetSettingsQuery, List<SettingDto>>
    {
        #region Fields
        private readonly ISettingRepository _settingRepository;
        #endregion
        #region Constructors
        public GetSettingQueryHandler(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }
        #endregion
        #region Methods
        public async Task<List<SettingDto>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _settingRepository.GetAll(asNoTracking: true, cancellationToken: cancellationToken);
            return settings.Adapt<List<SettingDto>>();
        }
        #endregion

    }
}
