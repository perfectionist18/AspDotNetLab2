using AspDotNetLab2.Services;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspDotNetLab2.Middleware
{
    public class GeneralCounterServiceMiddleware
    {
        private readonly RequestDelegate _next;

        public GeneralCounterServiceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, GeneralCounterService general)
        {
            if (context.Request.Path.Value.ToLower() == "/services/general-counter")
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                general.Increment();
                await context.Response.WriteAsync($"Всього запитів: {general.Count}");
            }
            else
            {
                general.Increment();
                await _next.Invoke(context);
            }
        }
    }
}
