using Microsoft.AspNetCore.Http;
using System;

namespace Monitor.Server.Entities
{
    public static class Extensions
    {
        public static string GetOrigin(this HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.Headers["Origin"]))
                return context.Request.Headers["Origin"];
            else if (!string.IsNullOrEmpty(context.Request.Headers["Referer"]))
            {
                var uri = new Uri(context.Request.Headers["Referer"]);

                return $"{uri.Scheme}://{uri.Host}:{uri.Port}";
            }
            else
                return string.Empty;
        }
    }
}
