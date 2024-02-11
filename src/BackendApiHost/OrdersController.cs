using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using DataAccessLib.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BackendApiHost
{
    [Authorize("RequireInteractiveUser")]
    //[ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly DesktopEvalDBContext _dbContext;
        private static readonly List<Order> __ordersInMemory = new List<Order>()
        {
            new Order { Id = Order.NewId(), Date = DateTimeOffset.UtcNow, Name = "Remote Orders API", User = "bob" },
            new Order { Id = Order.NewId(), Date = DateTimeOffset.UtcNow.AddHours(1), Name = "Remote Order # 2", User = "bob" },
            new Order { Id = Order.NewId(), Date = DateTimeOffset.UtcNow.AddHours(4), Name = "Remote Order # 3", User = "alice" }
        };

        public OrdersController(ILogger<OrdersController> logger, DesktopEvalDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAll()
        {
            var h = HttpContext;
            _logger.LogInformation("GetAll");
            //var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(
            //    new[]
            //{
            //                "openid"
            //                ,"orders.read"
            //}
            //);
            //Debug.WriteLine($"access token-{accessToken}");


            //var ordersFromStorage = new List<Order>();
            //_dbContext.Orders.Include(a => a.PreOrder).Take(10).ToList().ForEach(a => ordersFromStorage.Add(new Order()
            //{ Id = a.OrderId, Name = a.PreOrder.InputAddress, User = a.UserId, Date = a.CreateDateTime }));
            //return Ok(ordersFromStorage.AsEnumerable());

            return Ok(__ordersInMemory.AsEnumerable());
        }
    }

    public class Order
    {
        static int _nextId = 1;
        public static int NewId()
        {
            return _nextId++;
        }

        public long Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
    }

}