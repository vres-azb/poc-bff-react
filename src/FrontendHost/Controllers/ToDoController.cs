using System.Diagnostics;
using Duende.Bff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;

namespace poc_bff.Controllers;

//[ApiController]
[BffApi]
public class ToDoController : ControllerBase
{
    private readonly ILogger<ToDoController> _logger;
    private readonly ITokenAcquisition tokenAcquisition;
    //private readonly IAuthorizationHeaderProvider authorizationHeaderProvider;

    private static readonly List<ToDo> __data = new List<ToDo>()
        {
            new ToDo { Id = ToDo.NewId(), Date = DateTimeOffset.UtcNow, Name = "Demo (local) ToDo API", User = "bob" },
            new ToDo { Id = ToDo.NewId(), Date = DateTimeOffset.UtcNow.AddHours(1), Name = "My Task # 1", User = "bob" },
            new ToDo { Id = ToDo.NewId(), Date = DateTimeOffset.UtcNow.AddHours(4), Name = "Another Task", User = "alice" }
        };

    public ToDoController(ILogger<ToDoController> logger
        //,IAuthorizationHeaderProvider authorizationHeaderProvider
        //, ITokenAcquisition tokenAcquisition
        )
    {
        _logger = logger;
        //this.authorizationHeaderProvider = authorizationHeaderProvider;
        //this.tokenAcquisition = tokenAcquisition;
    }

    [HttpGet("todos")]
    public async Task<IActionResult> GetAll()
    {
        var http = this.HttpContext.Request;
        string[] scopes = new string[] { "https://iamvresdnadev001.onmicrosoft.com/poc-bff-api/orders.read" };
        try
        {
            //var t = await this.tokenAcquisition.GetAccessTokenForUserAsync(scopes);
            //.CreateAuthorizationHeaderForUserAsync(scopes);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }

        _logger.LogInformation("GetAll");

        // TODO: Remove this later, calling an eternal API needs a different approach
        //HttpClient client = new HttpClient();
        //client.BaseAddress = new Uri("https://localhost:5020/");
        //var todos = await client.GetFromJsonAsync<List<ToDo>>("orders");
        //return Ok(todos.AsEnumerable());

        return Ok(__data.AsEnumerable());
    }

    [HttpGet("todos/{id}")]
    public IActionResult Get(int id)
    {
        var item = __data.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();

        _logger.LogInformation("Get {id}", id);
        return Ok(item);
    }

    [HttpPost("todos")]
    public IActionResult Post([FromBody] ToDo model)
    {
        model.Id = ToDo.NewId();
        model.User = $"{User.FindFirst("sub").Value} ({User.FindFirst("name").Value})";

        __data.Add(model);
        _logger.LogInformation("Add {name}", model.Name);

        return Created(Url.Action(nameof(Get), new { id = model.Id }), model);
    }

    [HttpPut("todos/{id}")]
    public IActionResult Put(int id, ToDo model)
    {
        var item = __data.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();

        item.Date = model.Date;
        item.Name = model.Name;

        _logger.LogInformation("Update {name}", model.Name);

        return NoContent();
    }

    [HttpDelete("todos/{id}")]
    public IActionResult Delete(int id)
    {
        var item = __data.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();

        __data.Remove(item);
        _logger.LogInformation("Delete {id}", id);

        return NoContent();
    }
}

public class ToDo
{
    static int _nextId = 1;
    public static int NewId()
    {
        return _nextId++;
    }

    public int Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public string Name { get; set; }
    public string User { get; set; }
}