var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors-nlb", policyBuilder => {
        //policyBuilder.WithOrigins("https://localhost:7080");
        policyBuilder.AllowAnyOrigin();
    });
});

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseCors();

app.MapReverseProxy();
app.MapHealthChecks("status");
app.Run();