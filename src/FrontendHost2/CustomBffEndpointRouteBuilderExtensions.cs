using Duende.Bff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Duende.Bff.Endpoints;
using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.Extensions.Logging;

namespace poc_bff
//namespace Microsoft.AspNetCore.Builder
{
    public static class CustomBffEndpointRouteBuilderExtensions
    {
        internal static bool _licenseChecked;

        private static Task ProcessWith<T>(HttpContext context)
            where T : IBffEndpointService
        {
            var service = context.RequestServices.GetRequiredService<T>();
            return service.ProcessRequestAsync(context);
        }

        /// <summary>
        /// Adds the BFF management endpoints (login, logout, logout notifications)
        /// </summary>
        /// <param name="endpoints"></param>
        public static void MapCustomBffManagementEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapBffManagementLoginEndpoint();
            endpoints.MapBffManagementLogoutEndpoint();
            endpoints.MapBffManagementUserEndpoint();
            endpoints.MapBffManagementBackchannelEndpoint();
        }

        /// <summary>
        /// Adds the login BFF management endpoint
        /// </summary>
        /// <param name="endpoints"></param>
        public static void MapBffManagementLoginEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.CheckLicense();

            var options = endpoints.ServiceProvider.GetRequiredService<BffOptions>();

            endpoints.MapGet(options.LoginPath, ProcessWith<ILoginService>);
        }

        /// <summary>
        /// Adds the logout BFF management endpoint
        /// </summary>
        /// <param name="endpoints"></param>
        public static void MapBffManagementLogoutEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.CheckLicense();

            var options = endpoints.ServiceProvider.GetRequiredService<BffOptions>();

            endpoints.MapGet(options.LogoutPath, ProcessWith<ILogoutService>);
        }

        /// <summary>
        /// Adds the user BFF management endpoint
        /// </summary>
        /// <param name="endpoints"></param>
        public static void MapBffManagementUserEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.CheckLicense();

            var options = endpoints.ServiceProvider.GetRequiredService<BffOptions>();

            endpoints.MapGet(options.UserPath, ProcessWith<IUserService>)
                .AsBffApiEndpoint();
        }

        /// <summary>
        /// Adds the back channel BFF management endpoint
        /// </summary>
        /// <param name="endpoints"></param>
        public static void MapBffManagementBackchannelEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.CheckLicense();

            var options = endpoints.ServiceProvider.GetRequiredService<BffOptions>();

            endpoints.MapPost(options.BackChannelLogoutPath, ProcessWith<IBackchannelLogoutService>);
        }

        internal static void CheckLicense(this IEndpointRouteBuilder endpoints)
        {
            //if (_licenseChecked == false)
            //{
            //    var loggerFactory = endpoints.ServiceProvider.GetRequiredService<ILoggerFactory>();
            //    var options = endpoints.ServiceProvider.GetRequiredService<BffOptions>();

            //    LicenseValidator.Initalize(loggerFactory, options);
            //    LicenseValidator.ValidateLicense();
            //}

            _licenseChecked = true;
        }
    }
}
