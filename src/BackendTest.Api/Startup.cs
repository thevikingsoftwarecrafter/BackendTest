using BackendTest.Api.Config;
using BackendTest.Infrastructure.CrossCutting.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackendTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            BackendTestApiConfiguration.ConfigureServices(services, Environment, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            BackendTestApiConfiguration.Configure(app, provider, host =>
            {
                if (env.IsDevelopment())
                {
                    host.UseDeveloperExceptionPage();
                }
                else
                {
                    host.UseExceptionHandler("/home/error");
                }

                host.UseStaticFiles();
                host.UseMiddleware<LogContextMiddleware>(); // This should go in this order

                return host;
            });
        }
    }
}
