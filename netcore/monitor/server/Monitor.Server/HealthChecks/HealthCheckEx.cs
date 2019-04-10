using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;

namespace Monitor.Server.HealthChecks
{
    public static class HealthCheckEx
    {
        public static HealthCheckOptions BuildOptions()
        {
            var options = new HealthCheckOptions();

            options.ResponseWriter = async (c, r) =>
            {
                c.Response.ContentType = "application/json";

                var result = JsonConvert.SerializeObject(new
                {
                    status = r.Status.ToString(),
                    errors = r.Entries.Select(e => new { key = e.Key, value = e.Value.Status.ToString() })
                });
                await c.Response.WriteAsync(result);
            };

            return options;
        }
    }
}
