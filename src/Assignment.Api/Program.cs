using System.Text;
using Assignment.Api.Services;
using Assignment.Application;
using Assignment.Application.Interfaces.Services;
using Assignment.Infrastructure;
using Assignment.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

// Configure Services
var builder = WebApplication.CreateBuilder(args);
string corsurl = builder.Configuration.GetSection("UpStreamUrl:URL").Value;
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(corsurl).AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

// TODO: Remove this line if you want to return the Server header
builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);
builder.Services.AddSingleton(builder.Configuration);

// Adds in Application dependencies
builder.Services.AddApplication(builder.Configuration);
// Adds in Infrastructure dependencies
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor();


builder.Services.AddHealthChecks();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
});

builder.Services.AddScoped<IPrincipalService, PrincipalService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Assignment.Api", Version = "v1" });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


// Configure Application
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Assignment.Api v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseCors();

app.Use(async (httpContext, next) =>
{
    var apiMode = httpContext.Request.Path.StartsWithSegments("/api");
    if (apiMode)
    {
        httpContext.Request.Headers[HeaderNames.XRequestedWith] = "XMLHttpRequest";
    }
    await next();
});

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
    endpoints.MapControllers();
});

app.Run();
