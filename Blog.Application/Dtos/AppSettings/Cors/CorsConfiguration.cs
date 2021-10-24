using System.Collections.Generic;
using Blog.Shared.Markers.Configurations;

namespace Blog.Application.Dtos.AppSettings.Cors
{
    public class CorsConfiguration :IAppSetting
    {
        public bool AllowAllOrigins { get; set; }
        public bool AllowAllHeaders { get; set; }
        public bool AllowAllMethods { get; set; }
        public string Origins { get; set; }
        public string Headers { get; set; }
        public string Methods { get; set; }
    }
}