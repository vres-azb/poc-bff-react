var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddCors(options =>
{
    // HACK: This is only to demonstrate a CORS policy in code
    options.AddPolicy("cors-nlb", policyBuilder => {
        policyBuilder.WithOrigins("https://localhost:7077");
    });
});

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseCors();

app.MapReverseProxy();
app.MapHealthChecks("status");
app.Run();