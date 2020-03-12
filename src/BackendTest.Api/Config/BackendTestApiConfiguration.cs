using System;
using System.Net.Http;
using System.Reflection;
using AspNetCoreRateLimit;
using BackendTest.Api.Services;
using BackendTest.Api.V1.Behaviors;
using BackendTest.Domain.Queries.IntelligentBillboard;
using BackendTest.Domain.Repositories;
using BackendTest.Domain.Services;
using BackendTest.Infrastructure.CrossCutting.Swagger;
using BackendTest.Infrastructure.Data.DBContext;
using BackendTest.Infrastructure.Data.Repositories;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BackendTest.Api.Config
{
    public static class BackendTestApiConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
        {
            services.AddIpRateLimiting(configuration);
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();
            services.AddMediatR(typeof(GetIntelligentBillboardHandler).GetTypeInfo().Assembly);
            services.AddRepositories();
            services.AddVersioning();
            services.AddSwagger();
            services.AddPipelineBehaviors();
            services.AddProblemDetails(pbo => ConfigureProblemDetails(pbo, environment));
            services.AddDbContext<beezycinemaContext>(options => options.UseSqlServer(configuration.GetConnectionString("beezycinema"), m => m.MigrationsAssembly("BackendTest.Infrastructure.Data")));
            services.AddSingleton<IDateTimeService, DateTimeService>();

            return services;
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IQueriedMoviesRepository, QueriedMoviesRepository>();
        }


        public static void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider, Func<IApplicationBuilder, IApplicationBuilder> configureHost)
        {
            configureHost(app);
            app.UseIpRateLimiting();
            app.UseProblemDetails();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }

        private static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
        }

        private static void AddSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments -> do we need to do this? I don't think so.
                    // options.IncludeXmlComments(XmlCommentsFilePath);

                    options.EnableAnnotations();
                });
        }
        
        private static void AddIpRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            // needed to load configuration from appsettings.json
            services.AddOptions();

            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();

            //load general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

            // inject counter and rules stores
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            // https://github.com/aspnet/Hosting/issues/793
            // the IHttpContextAccessor service is not registered by default.
            // the clientId/clientIp resolvers use it.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }

        public static void AddPipelineBehaviors(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorPipelineBehavior<,>));
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(GetIntelligentBillboardHandler))
                .AddClasses(@class => @class.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces());
        }

        private static void ConfigureProblemDetails(ProblemDetailsOptions options, IWebHostEnvironment environment)
        {
            // This is the default behavior; only include exception details in a development environment.
            options.IncludeExceptionDetails = ctx => environment.IsDevelopment();

            // This will map NotImplementedException to the 501 Not Implemented status code.
            options.Map<NotImplementedException>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status501NotImplemented));

            // This will map HttpRequestException to the 503 Service Unavailable status code.
            options.Map<HttpRequestException>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status503ServiceUnavailable));

            // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
            // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
            options.Map<Exception>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status500InternalServerError));
        }
    }
}