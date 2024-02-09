using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLib.Persistence;
using DataAccessLib.Persistence.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

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

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddJwtBearer(options =>
            //   {
            //       //options.Authority = "https://demo.duendesoftware.com";
            //       //options.Audience = "api";

            //       options.MapInboundClaims = false;

            //   });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(
                options =>
                {
                    _config.Bind("Auth:AzureAdB2C", options);
                    options.Events ??= new JwtBearerEvents();
                    options.MapInboundClaims = false;

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateAudience = false,
                        //ValidTypes = new[] {"at+jwt"},
                        NameClaimType = "name",
                        RoleClaimType = "role",
                    };

                }, options =>
                {
                    _config.Bind("Auth:AzureAdB2C", options);
                    //options.SignInScheme = "Cookies";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiCaller", policy =>
                {
                    policy.RequireClaim("aud");
                    //policy.RequireClaim("scope", "api");
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
