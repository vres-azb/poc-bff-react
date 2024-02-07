using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace BackendApiHost
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services
                .AddMicrosoftIdentityWebApiAuthentication(Configuration.GetSection("AuthConfig"), "AzureAdB2C", JwtBearerDefaults.AuthenticationScheme)
                .EnableTokenAcquisitionToCallDownstreamApi(options =>
                {
                    //options.ClientSecret = "";
                    options.LogLevel = Microsoft.Identity.Client.LogLevel.Info;
                })
                .AddInMemoryTokenCaches();

            /*
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                //.AddJwtBearer(options =>
                //{
                //    //options.Authority = "https://demo.duendesoftware.com";
                //    options.Authority = "https://iamvresdnadev001.b2clogin.com/iamvresdnadev001.onmicrosoft.com/b2c_1a_signup_signin/v2.0/";

                //    Configuration.Bind("AuthConfig:AzureAdB2C", options);

                //    options.IncludeErrorDetails = true;

                //    options.MapInboundClaims = false;
                //    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents();
                //    options.Events.OnAuthenticationFailed = async (arg) =>
                //    {
                //        var s = arg;
                //        await Task.CompletedTask;
                //    };
                //    options.Events.OnChallenge = async (arg) =>
                //    {
                //        var s = arg;
                //        await Task.CompletedTask;
                //    };
                //    options.Events.OnMessageReceived = async (arg) =>
                //    {
                //        var s = arg;
                //        await Task.CompletedTask;
                //    };
                //    options.Events.OnTokenValidated = async (arg) =>
                //    {
                //        var s = arg;
                //        await Task.CompletedTask;
                //    };
                //})
                .AddMicrosoftIdentityWebApi(options =>
                {
                    Configuration.Bind("AuthConfig:AzureAdB2C", options);
                    //options.ForwardDefault = "Cookies";

                    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents();
                    options.Events.OnForbidden = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };
                    options.Events.OnAuthenticationFailed = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };
                    options.Events.OnChallenge = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };
                    options.Events.OnMessageReceived = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };
                    options.Events.OnTokenValidated = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };
                },
                options =>
                {
                    options.SignInScheme = "Cookies";
                    Configuration.Bind("AuthConfig:AzureAdB2C", options);
                    if (options.Events!=null)
                    {
                        return;
                    }
                    options.Events = new Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents();
                    options.Events.OnAuthenticationFailed = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };

                    options.Events.OnAuthorizationCodeReceived = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };

                    options.Events.OnRedirectToIdentityProvider = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };

                    options.Events.OnRemoteFailure = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };

                    options.Events.OnTokenResponseReceived = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };

                    options.Events.OnTokenValidated = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };

                    options.Events.OnUserInformationReceived = async (arg) =>
                    {
                        var s = arg;
                        await Task.CompletedTask;
                    };
                }, subscribeToJwtBearerMiddlewareDiagnosticsEvents: true);

            */

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiCaller", policy =>
                {
                    //policy.RequireClaim("scope", "api");
                    //policy.RequireClaim("aud", "794b5328-825d-4f14-bef9-9e4d582f2736");
                    policy.RequireClaim("aud");

                });

                options.AddPolicy("RequireInteractiveUser", policy =>
                {
                    policy.RequireClaim("sub");
                    //policy.RequireClaim("https://iamvresdnadev001.onmicrosoft.com/poc-bff-api/orders.read");
                });
            });

            services.AddControllers();
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
                    //.RequireAuthorization("RequireInteractiveUser")
                    ;
            });
        }
    }
}