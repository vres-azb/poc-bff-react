using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace BackendApiHost
{
    [Authorize("RequireInteractiveUser")]
    [RequiredScope(scopeRequiredByAPI)]
    public class OrdersController : ControllerBase
    {
        const string scopeRequiredByAPI = "orders.read";

        private readonly ILogger<OrdersController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITokenAcquisition _tokenAcquisition;
        private static readonly Order[] __orders = new[]{
            new Order{Id = Order.NewId()},
            new Order{Id = Order.NewId()},
            new Order{Id = Order.NewId()}
        };

        public OrdersController(ILogger<OrdersController> logger, IHttpContextAccessor contextAccessor, ITokenAcquisition tokenAcquisition)
        {
            this._logger = logger;

            this._contextAccessor = contextAccessor;
            this._tokenAcquisition = tokenAcquisition;
            string owner = this._contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAll()
        {
            var authScheme = this._tokenAcquisition.GetEffectiveAuthenticationScheme("Cookies1");
            var at = await this._tokenAcquisition.GetAccessTokenForAppAsync("https://iamvresdnadev001.onmicrosoft.com/poc-bff-api/orders.read");

            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(
                new[]
           {
                "openid"
                ,"orders.read"
           }
           );
            Debug.WriteLine($"access token-{accessToken}");

            this._logger.LogInformation("GetAllOrders");
            return Ok(__orders);
        }
    }

    public class Order
    {
        static int _nextId = 1;
        public static int NewId()
        {
            return _nextId++;
        }

        public int Id { get; set; }
    }
}