using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using DataAccessLib.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BackendApiHost
{
    //[Authorize]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly DesktopEvalDBContext _dbContext;
        private static readonly List<ToDo> __data = new List<ToDo>()
        {
            new ToDo { Id = ToDo.NewId(), Date = DateTimeOffset.UtcNow, Name = "Demo ToDo API", User = "bob" },
            new ToDo { Id = ToDo.NewId(), Date = DateTimeOffset.UtcNow.AddHours(1), Name = "My Task # 1", User = "bob" },
            new ToDo { Id = ToDo.NewId(), Date = DateTimeOffset.UtcNow.AddHours(4), Name = "Another Task", User = "alice" }
        };

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


            var todos = new List<ToDo>();
            //_dbContext.Orders.Include(a => a.PreOrder).Take(10).ToList().ForEach(a => todos.Add(new ToDo()
            //{ Id = a.OrderId, Name = a.PreOrder.InputAddress, User = a.UserId, Date = a.CreateDateTime }));

            return Ok(todos.AsEnumerable());
        }
    }
}
