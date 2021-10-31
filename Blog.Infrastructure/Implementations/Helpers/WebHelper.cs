using System;
using System.Linq;
using System.Net;
using Blog.Application.Dtos.AppSettings.WebHosting;
using Blog.Infrastructure.Contracts.Helpers;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Blog.Infrastructure.Implementations.Helpers
{
    public class WebHelper  :IWebHelper ,IScopedLifeTime
    {

        #region Const

        private const string NullIpAddress = "::1";

        #endregion
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HostConfiguration _hostConfiguration;
       

        #endregion

        #region Constructors

                public WebHelper(IHttpContextAccessor httpContextAccessor, HostConfiguration hostConfiguration)
                {
                    _httpContextAccessor = httpContextAccessor;
                    _hostConfiguration = hostConfiguration;
                }

        #endregion
        #region Methods

        protected virtual bool IsRequestAvailable()
        {
            if (_httpContextAccessor?.HttpContext == null)
                return false;

            try
            {
                if (_httpContextAccessor.HttpContext.Request == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public string GetUserAgent()
        {
            var ua = _httpContextAccessor?.HttpContext?.Request?.Headers["User-Agent"].ToString();
            if (string.IsNullOrEmpty(ua))
            {
                ua = _httpContextAccessor?.HttpContext?.Request?.Headers[HeaderNames.UserAgent].ToString();
            }

            return ua;
        }

        public string GetIpAddress()
        {
            if (!IsRequestAvailable())
                return string.Empty;

            var result = string.Empty;
            try
            {
                //first try to get IP address from the forwarded header
                if (_httpContextAccessor.HttpContext.Request.Headers != null)
                {
                    //the X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a client
                    //connecting to a web server through an HTTP proxy or load balancer
                    var forwardedHttpHeaderKey = "X-FORWARDED-FOR";
                    if (!string.IsNullOrEmpty(_hostConfiguration.ForwardedHttpHeader))
                    {
                        //but in some cases server use other HTTP header
                        //in these cases an administrator can specify a custom Forwarded HTTP header (e.g. CF-Connecting-IP, X-FORWARDED-PROTO, etc)
                        forwardedHttpHeaderKey = _hostConfiguration.ForwardedHttpHeader;
                    }

                    var forwardedHeader = _httpContextAccessor.HttpContext.Request.Headers[forwardedHttpHeaderKey];
                    if (!StringValues.IsNullOrEmpty(forwardedHeader))
                        result = forwardedHeader.FirstOrDefault();
                }

                //if this header not exists try get connection remote IP address
                if (string.IsNullOrEmpty(result) && _httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
                    result = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch
            {
                return string.Empty;
            }

            //some of the validation
            if (result != null && result.Equals(IPAddress.IPv6Loopback.ToString(), StringComparison.InvariantCultureIgnoreCase))
                result = IPAddress.Loopback.ToString();

            //"TryParse" doesn't support IPv4 with port number
            if (IPAddress.TryParse(result ?? string.Empty, out var ip))
                //IP address is valid 
                result = ip.ToString();
            else if (!string.IsNullOrEmpty(result))
                //remove port
                result = result.Split(':').FirstOrDefault();

            return result;
        }
        #endregion
     
    }
}