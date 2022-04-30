using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PrintShop.Identity;
using Serilog.Context;

namespace PrintShop.Extensions
{
    public class LogUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AdUserInfo _adUserInfo;
        // Enriches logs to include name and username of user
        public LogUserMiddleware(RequestDelegate next, AdUserInfo adUserInfo)
        {
            _next = next;
            _adUserInfo = adUserInfo;
        }

        public async Task Invoke(HttpContext context)
        {
            LogContext.PushProperty("User", new
            {
                Identity = new
                {
                    _adUserInfo.UserName,
                    _adUserInfo.Name
                }
            }, true);
            await _next(context);
        }
    }
}
