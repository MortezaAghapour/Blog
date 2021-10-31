using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Shared.Markers.Configurations;

namespace Blog.Application.Dtos.AppSettings.Logging
{
    public class ElmahConfiguration:IAppSetting
    {
        public string Path { get; set; }
    }
}
