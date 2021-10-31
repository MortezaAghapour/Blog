using Blog.Shared.Markers.Configurations;

namespace Blog.Application.Dtos.AppSettings.WebHosting
{
    public class HostConfiguration:IAppSetting
    {
        public string ForwardedHttpHeader { get; set; }
        public bool UseHttpClusterHttps { get; set; }
        public bool UseHttpXForwardedProto { get; set; }
    }
}