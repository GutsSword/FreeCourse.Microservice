using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json");

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme",opt =>
{
    opt.Audience = "resource_gateway" ;
    opt.Authority = builder.Configuration["IdentityServerUrl"]; ;
    opt.RequireHttpsMetadata = false;
});

//builder.Services.AddAuthentication();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
}

//app.UseAuthentication();

app.UseAuthorization();

await app.UseOcelot();

app.Run();
