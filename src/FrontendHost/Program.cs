//using Auth0.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using poc_bff;
//using Microsoft.Identity.Web.Client.TokenCacheProviders;

var builder = WebApplication.CreateBuilder(args);

// TODO: this is an example of EXTENSIBILITY to add custom claims
//builder.Services.AddTransient<IClaimsTransformation, CustomB2CClaimsTransformation>();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddControllers();
builder.Services.AddBff(options =>
{
    // per https://datatracker.ietf.org/doc/html/rfc7009#section-2
    // AZ B2C does not offer a revokation endpoint
    options.RevokeRefreshTokenOnLogout = false;

    // Open this url to config
    // https://IAM.b2clogin.com/IAM.onmicrosoft.com/b2c_1a_signup_signin/v2.0/.well-known/openid-configuration
})
.AddServerSideSessions()
.AddRemoteApis();

builder.Services
.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
    options.DefaultSignOutScheme = "oidc";
})
.AddCookie("Cookies", options =>
{
    options.ForwardDefault = "Cookies";
    options.Cookie.Name = "__Host-bff-poc"; // secure cookie per spec https://datatracker.ietf.org/doc/html/draft-ietf-httpbis-cookie-prefixes-00#section-3.2

    // TODO: enable this to test cookie expiration, use the token lifetime instead (check the claims), 10 mins max
    //options.ExpireTimeSpan = TimeSpan.FromSeconds(60);
    options.SlidingExpiration = false;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.Path = "/";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    options.Cookie.HttpOnly = true;

    // HACK: the configuration above produces a cookie with the following values:
    // Set-Cookie: __Host-bff-poc=a123; path=/; Secure; HttpOnly; SameSite=Lax
})
.AddOpenIdConnect("oidc", options =>
{
    options.SignInScheme = "Cookies";
    builder.Configuration.Bind("Auth:AzB2C", options);
    //builder.Configuration.Bind("Auth:Auth0", options);
    //builder.Configuration.Bind("Auth:Duende", options);

    options.ResponseType = OpenIdConnectResponseType.Code; // "code";
    options.ResponseMode = OpenIdConnectResponseMode.Query;// "query";

    options.GetClaimsFromUserInfoEndpoint = true;
    options.MapInboundClaims = false;
    options.SaveTokens = true;

    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("offline_access");

    // HACK: This is needed when using AzB2
    options.Scope.Add(options.ClientId);

    options.TokenValidationParameters = new()
    {
        NameClaimType = "name",
        RoleClaimType = "role",
    };

})
//.AddMicrosoftIdentityWebApp(options =>
//{
//    builder.Configuration.Bind("Auth:AzB2C", options);

//    options.SignInScheme = "Cookies";
//    //options.SignInScheme = OpenIdConnectDefaults.AuthenticationScheme;

//    //options.ClientSecret = "ww88Q~8ypSVF0mnQh5jXyxLWzCA6UM44gkspxceU";

//    //options.ForwardDefault = "oidc";
//    //options.ForwardSignIn = "oidc";
//    //options.ForwardSignOut = "oidc";

//    options.Events = new OpenIdConnectEvents();
//    options.Events.OnAuthenticationFailed = async (arg) =>
//    {
//        var s = arg;
//        await Task.CompletedTask;
//    };

//    options.Events.OnAuthorizationCodeReceived = async (arg) =>
//    {
//        var s = arg;
//        await Task.CompletedTask;
//    };

//    options.Events.OnRedirectToIdentityProvider = async (arg) =>
//    {
//        var s = arg;
//        await Task.CompletedTask;
//    };

//    options.Events.OnRemoteFailure = async (arg) =>
//    {
//        var s = arg;
//        await Task.CompletedTask;
//    };

//    options.Events.OnTokenResponseReceived = async (arg) =>
//    {
//        var s = arg;
//        await Task.CompletedTask;
//    };

//    options.Events.OnTokenValidated = async (arg) =>
//    {
//        var s = arg;
//        await Task.CompletedTask;
//    };

//    options.Events.OnUserInformationReceived = async (arg) =>
//    {
//        var s = arg;
//        await Task.CompletedTask;
//    };

//}
//, options =>
//{
//    options.ForwardDefault = "Cookies";
//    builder.Configuration.Bind("Auth:AzB2C", options);

//    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents();
//    options.Events.OnSignedIn = async (arg) =>
//    {
//        var s = arg;
//        await Task.CompletedTask;
//    };

//}
//, "oidcAzB2C"
//, "CookiesAzB2C"
//).EnableTokenAcquisitionToCallDownstreamApi(
//    new string[]
//    {
//        "https://iamvresdnadev001.onmicrosoft.com/poc-bff-api/orders.read"
//    }
//)
//.AddInMemoryTokenCaches()
;


//builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "Auth:AzB2C")
//                    .EnableTokenAcquisitionToCallDownstreamApi(new string[]
//                    {
//                        "orders.read"
//                        //builder.Configuration["TodoList:TodoListScope"]
//                    })
//                    .AddInMemoryTokenCaches()
//                    ;

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
app.MapRemoteBffApiEndpoint("/orders1", "https://iamvresdnadev001.onmicrosoft.com/poc-bff-api")
    .RequireAccessToken(Duende.Bff.TokenType.User);

app.MapRemoteBffApiEndpoint("/orders", "https://localhost:5020/orders")
    .RequireAccessToken(Duende.Bff.TokenType.User)
    //.RequireScope(new[] { "orders.read" })
    ;

app.MapFallbackToFile("index.html");

app.Run();