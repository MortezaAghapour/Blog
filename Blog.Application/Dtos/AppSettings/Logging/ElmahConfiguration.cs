using Blog.Shared.Markers.Configurations;

namespace Blog.Application.Dtos.AppSettings.Logging
{
    public class ElmahConfiguration:IAppSetting
    {
        public string Path { get; set; }
    }
}
