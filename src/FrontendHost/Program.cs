using System.IdentityModel.Tokens.Jwt;
using Duende.Bff.Yarp;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.DataProtection;
using poc_bff;
using DataAccessLib.Persistence.Repository;
using DataAccessLib.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// TODO: this is an example of EXTENSIBILITY to add custom claims
//builder.Services.AddTransient<IClaimsTransformation, CustomB2CClaimsTransformation>();

 builder.Services.AddDbContext<CustomDataProtectionKeyContext>(options =>
 {
     options.UseSqlServer("Server=tcp:sql_server2022,1433;UID=sa;Password=P@ssw0rd;Initial Catalog=master;TrustServerCertificate=true;");
 });

builder.Services.Configure<CustomDataProtectionKeyContext>(options =>
{
    builder.Configuration.Bind("DesktopEvalDBSettings:DefaultConnection", options);
});

builder.Services.AddDataProtection()
    .SetApplicationName("poc-bff")
    //.PersistKeysToFileSystem(new DirectoryInfo("./"))
    .PersistKeysToDbContext<CustomDataProtectionKeyContext>()
    ;

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddControllers();
builder.Services.AddBff(options =>
{
    // per https://datatracker.ietf.org/doc/html/rfc7009#section-2
    // AZ B2C does not offer a revokation endpoint
    options.RevokeRefreshTokenOnLogout = false;

    // OIDC metadata endpoint
    // https://IAM.b2clogin.com/IAM.onmicrosoft.com/b2c_1a_signup_signin/v2.0/.well-known/openid-configuration
})
.AddServerSideSessions()
.AddRemoteApis();

builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromSeconds(35);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

builder.Services
    .AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromSeconds(35);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    })
    .AddDistributedSqlServerCache(options =>
    {
        options.ConnectionString = "Server=tcp:sql_server2022,1433;UID=sa;Password=P@ssw0rd;Initial Catalog=master;TrustServerCertificate=true;";
        options.SchemaName = "dist_cache";
        options.TableName = "AppCache";
    })
    .AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "__Host-bff-poc"; // secure cookie per spec https://datatracker.ietf.org/doc/html/draft-ietf-httpbis-cookie-prefixes-00#section-3.2

        // TODO: enable this to test cookie expiration, use the token lifetime instead (check the claims), 10 mins max
        //options.ExpireTimeSpan = TimeSpan.FromSeconds(60);
        options.SlidingExpiration = false;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.Path = "/";
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        options.Cookie.HttpOnly = true;

        //HACK: the configuration above produces a cookie with the following values:
        // Set-Cookie: __Host-bff-poc=a123; path=/; Secure; HttpOnly; SameSite=Lax
    })
    .AddOpenIdConnect("oidc", options =>
    {
        builder.Configuration.Bind("AuthConfig:AzureAdB2C", options);

        options.SignInScheme = "Cookies";

        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.MapInboundClaims = false;
        options.SaveTokens = true;

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        //options.Scope.Add ("https://iamvresdnadev001.onmicrosoft.com/poc-bff-api/orders.read");
        options.Scope.Add("offline_access");


        options.GetClaimsFromUserInfoEndpoint = true;

        // Az B2C jwt-test-app
        options.Scope.Add(options.ClientId);

        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";

        options.TokenValidationParameters.ValidateAudience = false;
    })
//.AddJwtBearer("token", options =>
//{
//    builder.Configuration.Bind("AuthConfig:AzureAdB2C", options);
//    //options.Audience = "api";

//    options.MapInboundClaims = false;
//    options.TokenValidationParameters ??= new Microsoft.IdentityModel.Tokens.TokenValidationParameters();
//    options.TokenValidationParameters.ValidateAudience = false;
//});
;

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();
//app.MapBffManagementEndpoints();
app.MapCustomBffManagementEndpoints();

app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();

// TODO: validate local/yarp api usage
app.MapRemoteBffApiEndpoint("/orders", "https://localhost:5020/orders")
    .RequireAccessToken(Duende.Bff.TokenType.User);

app.MapFallbackToFile("index.html");

app.Run();