using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Web;
using TodoApp.Contexts;
using TodoApp.Models.Database;
using TodoApp.Repositories;
using TodoApp.Services;
using TodoApp.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var cookieSettings = builder.Configuration.GetRequiredSection(nameof(CookieSettings)).Get<CookieSettings>()!;
builder.Services.AddSingleton(Options.Create(cookieSettings));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(cookieOptions =>
    {
        cookieOptions.Cookie.HttpOnly = true;
        cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        cookieOptions.Cookie.SameSite = SameSiteMode.Strict;
        cookieOptions.LoginPath = cookieSettings.LoginPath;
        cookieOptions.LogoutPath = cookieSettings.LogoutPath;
        cookieOptions.AccessDeniedPath = cookieSettings.AccessDeniedPath;
        cookieOptions.ExpireTimeSpan = cookieSettings.CookieExpiry;
        cookieOptions.SlidingExpiration = true;
    });

builder.Services.AddSingleton<TodoService>();
builder.Services.AddSingleton<DatabaseRepository>();
builder.Services.AddDbContext<DatabaseContext>(options => 
    options.UseInMemoryDatabase(nameof(DatabaseContext)), 
    ServiceLifetime.Singleton);

LogManager.Setup().LoadConfigurationFromFile();
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("Angular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();