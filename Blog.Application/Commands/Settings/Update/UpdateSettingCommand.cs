using Blog.Application.Dtos.Settings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Settings.Update
{
    public class UpdateSettingCommand:IRequest<SettingDto>
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
