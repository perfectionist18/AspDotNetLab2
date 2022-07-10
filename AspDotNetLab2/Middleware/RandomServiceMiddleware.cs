using AspDotNetLab2.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspDotNetLab2.Middleware
{
    public class RandomServiceMiddleware
    {
        private readonly RequestDelegate _next;
        public RandomServiceMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, ICounter counter, RandomService randomService, GeneralCounterService general)
        {
            if (httpContext.Request.Path.Value.ToLower() == "/services/random")
            {
                httpContext.Response.ContentType = "text/html;charset=utf-8";
                await httpContext.Response.WriteAsync($"Випадкове число 1: {counter.Value}; Випадкове число 2: {randomService.Counter.Value}");
            }
            else
                await _next.Invoke(httpContext);
        }
    }
}
