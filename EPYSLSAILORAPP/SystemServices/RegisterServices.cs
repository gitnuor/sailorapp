using AutoMapper;
using EPYSLSAILORAPP.Infrastructure.Identity.Configuration;
using EPYSLSAILORAPP.Infrastructure.Identity.General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.IO.Compression;
using System.Text;

namespace EPYSLSAILORAPP.SystemServices
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<GlobalExceptionMiddleware>>();
            services.AddSingleton(typeof(ILogger), logger);
            //services.AddSingleton(typeof(IMapper));
            //services.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            #region Health Check Service    ---- Need to change query & url from Setting
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json");

            var config = configuration.Build();

            // Attribute routing.
            //config.MapHttpAttributeRoutes();

            //// Convention-based routing.
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            #endregion

            #region Swagger, CORS, Authentication & Brotli Compression Service
            services.AddEndpointsApiExplorer();
            
            var identityData = builder.Configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
            services.AddOptions();
            services.Configure<IdentitySettings>(x => x.Issuer = identityData.Issuer.ToString());
            services.Configure<IdentitySettings>(x => x.Audience = identityData.Audience.ToString());
            services.Configure<IdentitySettings>(x => x.Encryptkey = identityData.Encryptkey.ToString());
            services.Configure<IdentitySettings>(x => x.ExpirationMinutes = identityData.ExpirationMinutes);
            services.Configure<IdentitySettings>(x => x.SecretKey = identityData.SecretKey);


            #region Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = config["IdentitySettings:Issuer"],
                       ValidAudience = config["IdentitySettings:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["IdentitySettings:SecretKey"]))
                   };
               });
            #endregion


            #region CORS

            var _appSettings = builder.Configuration.GetSection(nameof(CORS)).Get<CORS>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowedOrigins", builder => builder
                    .WithOrigins(_appSettings.AllowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                options.AddPolicy("AllowAnyOrigin", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                options.AddPolicy("CustomPolicy", builder => builder
                    .AllowAnyOrigin()
                    .WithMethods("Get")
                    .WithHeaders("Content-Type"));
            });
            #endregion

            #region Response Compression
            services.AddResponseCompression();
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            #endregion

            #endregion

            #region Api Versioning 
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
            #endregion

            #region FluentValidator Service
            //services.AddControllers()
            //   .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TechnicalSessionValidator>());
            #endregion

            #region Health check Service
            services.AddHealthChecks().AddUrlGroup(new Uri
                        ("http://localhost:47082/WeatherForecast"),
                         name: "base URL", failureStatus:
                         HealthStatus.Degraded).AddSqlServer(config.GetConnectionString("DefaultConnection"),
                            healthQuery: "select 1",
                            failureStatus: HealthStatus.Degraded,
                            name: "SQL Server");

            //services.AddHealthChecksUI(opt =>
            //{
            //    opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
            //    opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
            //    opt.SetApiMaxActiveRequests(1); //api requests concurrency    
            //    opt.AddHealthCheckEndpoint("default api", "/api/healthCheck"); //map health check api    
            //})
            //            .AddInMemoryStorage();

            #endregion

          
            return services;
        }
    }
}
