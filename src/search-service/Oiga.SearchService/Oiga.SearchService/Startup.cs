using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Oiga.Common.Middlewares;
using Oiga.Events.Enums;
using Oiga.SearchService.Data;
using Oiga.SearchService.Services;
using System.Text;

namespace Oiga.SearchService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SearchServiceDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddHttpClient();

            services.AddHttpClient();

            services.AddSingleton<IInputTokenizerService, InputTokenizerService>();

            services.AddMediatR(typeof(Startup));

            services.AddCors();
            services.AddControllers().AddDapr();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Oiga.SearchService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oiga.SearchService v1"));
            }

            app.UseCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options =>
            {
                options.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
            });

            app.UseAuthorization();

            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/dapr/subscribe", async context =>
                {
                    var logger = context.RequestServices.GetService<ILogger<Startup>>();
                    
                    logger.LogInformation("Subscribe endpoint called by Pubsub");

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        topic = Topics.UserCreated,
                        route = "/api/v1/daprevents/listen-user-events/",
                        pubsubname = "pubsub"
                    }), Encoding.UTF8);
                });
            });
        }
    }
}
