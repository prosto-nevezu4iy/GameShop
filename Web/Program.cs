using Infrastructure;
using Infrastructure.Data;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using Web.Config;
using Web.Configuration;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

Dependencies.ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

var identityConfig = builder.Configuration.GetSection("Identity").Get<Identity>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = identityConfig.Authority;

        options.ClientId = identityConfig.ClientId;
        options.ClientSecret = identityConfig.ClientSecret;
        options.ResponseType = "code";

        options.Scope.Add("profile");
        options.GetClaimsFromUserInfoEndpoint = true;

        options.SaveTokens = true;
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var applicationDbContext = scopedProvider.GetRequiredService<ApplicationDbContext>();
        await ApplicationDbContextSeed.SeedAsync(applicationDbContext, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseTransferAnonymousBasketToUser();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();
