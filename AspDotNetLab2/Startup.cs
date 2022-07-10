using AspDotNetLab2.Middleware;
using AspDotNetLab2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspDotNetLab2
{
    public class Startup
    {
        private IServiceCollection _services;

        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;
            services.AddTransient<TimerService>();
            services.AddScoped<ICounter, RandomCounter>();
            services.AddScoped<RandomService>();
            services.AddSingleton<GeneralCounterService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            app.Map("/services/list", ap => ap.Run(async (context) =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>Óñ³ ñåðâ³ñè</h1>");
                sb.Append("<table border=\"1\">");
                sb.Append("<tr><th>Òèï</th><th>Lifetime</th><th>Ðåàë³çàö³ÿ</th></tr>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync(sb.ToString());
            }));

            app.UseMiddleware<GeneralCounterServiceMiddleware>();
            app.UseMiddleware<TimerServiceMiddleware>();
            app.UseMiddleware<RandomServiceMiddleware>();
        }
    }
}
