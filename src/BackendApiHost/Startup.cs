using DataAccessLib.Persistence;
using DataAccessLib.Persistence.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackendApiHost
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddPersistence();

            services.Configure<DesktopEvalDBSettings>(_config.GetSection("DesktopEvalDBSettings"));

            services.AddAuthentication("token")
               .AddJwtBearer("token", options =>
               {
                   _config.Bind("AuthConfig:AzureAdB2C", options);
                   //options.Audience = "api";

                   options.MapInboundClaims = false;
                   options.TokenValidationParameters ??= new Microsoft.IdentityModel.Tokens.TokenValidationParameters();
                   options.TokenValidationParameters.ValidateAudience = false;
               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiCaller", policy =>
                {
                    //policy.RequireClaim("scope", "api");
                    policy.RequireClaim("sub");
                });

                options.AddPolicy("RequireInteractiveUser", policy =>
                {
                    policy.RequireClaim("sub");
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                 .RequireAuthorization("ApiCaller")
                 ;
            });
        }
    }
}