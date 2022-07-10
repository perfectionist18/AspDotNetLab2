using AspDotNetLab2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspDotNetLab2.Middleware
{
    public class TimerServiceMiddleware
    {
        private readonly RequestDelegate _next;
        TimerService _timeService;
        public TimerServiceMiddleware(RequestDelegate next, TimerService timeService)
        {
            _next = next;
            _timeService = timeService;
        }
        public async Task InvokeAsync(HttpContext context, GeneralCounterService general)
        {
            if (context.Request.Path.Value.ToLower() == "/services/timer")
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"Поточний час: {_timeService.GetTime()}");
            }
            else
                await _next.Invoke(context);
        }
    }
}
