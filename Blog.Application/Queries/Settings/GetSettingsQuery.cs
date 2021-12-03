using Blog.Application.Dtos.Settings;
using Blog.Domain.Entities.Settings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Settings
{
    public class GetSettingsQuery:IRequest<List<SettingDto>>
    {

    }
}
