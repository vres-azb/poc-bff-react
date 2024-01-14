using Duende.Bff.Yarp;

var builder = WebApplication.CreateBuilder(args);

// TODO: this is an example of EXTENSIBILITY to add custom claims
//builder.Services.AddTransient<IClaimsTransformation, CustomB2CClaimsTransformation>();

builder.Services.AddControllers();
builder.Services.AddBff(options =>
{
    options.LicenseKey = null;

    // per https://datatracker.ietf.org/doc/html/rfc7009#section-2
    // AZ B2C does not offer a revokation endpoint
    options.RevokeRefreshTokenOnLogout = false;

    // Open this url to confir,
    // https://IAM.b2clogin.com/IAM.onmicrosoft.com/b2c_1a_signup_signin/v2.0/.well-known/openid-configuration
})
.AddServerSideSessions()
.AddRemoteApis();

builder.Services.AddAuthentication(options =>
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
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.Path = "/";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    options.Cookie.HttpOnly = true;

    //HACK: the configuration above produces a cookie with the following values:
    // Set-Cookie: __Host-bff-poc=a123; path=/; Secure; HttpOnly; SameSite=Lax
})
.AddOpenIdConnect("oidc", async options =>
{
    builder.Configuration.Bind("App",options);

    options.ResponseType = "code";
    options.ResponseMode = "query";

    options.GetClaimsFromUserInfoEndpoint = true;
    options.MapInboundClaims = false;
    options.SaveTokens = true;

    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");

    // Az B2C jwt-test-app
    options.Scope.Add(options.ClientId);

    options.TokenValidationParameters = new()
    {
        NameClaimType = "name",
        RoleClaimType = "role"
    };

});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();
app.MapBffManagementEndpoints();

app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();

// TODO: validate local/yarp api usage
// app.MapRemoteBffApiEndpoint("/todos", "https://localhost:5020/todos")
//     .RequireAccessToken(Duende.Bff.TokenType.User);

app.MapFallbackToFile("index.html"); ;

app.Run();