using DataAccessLib.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApiHost
{
    [Authorize("RequireInteractiveUser")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly DesktopEvalDBContext _dbContext;

        public OrdersController(ILogger<OrdersController> logger, DesktopEvalDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAll()
        {
            //_logger.LogInformation("GetAll");
            //var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(
            //    new[]
            //{
            //                "openid"
            //                ,"orders.read"
            //}
            //);
            //Debug.WriteLine($"access token-{accessToken}");

            var usr = this.HttpContext.Request.Headers;

            var todos = new List<ToDo>();
            _dbContext.Orders
                .Include(a => a.PreOrder).Take(10).ToList()
                .ForEach(a => todos.Add(new ToDo()
                { Id = a.OrderId, Name = a.PreOrder.InputAddress, User = a.UserId, Date = a.CreateDateTime })
            );

            return Ok(todos.AsEnumerable());
        }
    }
}
