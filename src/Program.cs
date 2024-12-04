using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TempoMapRepository.Models.Identity;
using TempoMapRepository.Controllers;
using TempoMapRepository.Data.Context;
using TempoMapRepository.Data.Config;
using Microsoft.AspNetCore.Identity.UI.Services;
using TempoMapRepository.Services;
using Microsoft.AspNetCore.Mvc;
using TempoMapRepository.Policies.Requirements;
using TempoMapRepository.Policies.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddDefaultIdentity<User>(options =>//AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 10;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddApiEndpoints()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<AuthDbContext>(options =>
                        options.UseSqlite(builder.Configuration
                                            .GetConnectionString("DefaultConnection"))
                    );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddW3CLogging(logging =>
{
    // Log all W3C fields
    logging.LoggingFields = W3CLoggingFields.All;

    logging.AdditionalRequestHeaders.Add("x-forwarded-for");
    logging.AdditionalRequestHeaders.Add("x-client-ssl-protocol");
    logging.FileSizeLimit = 5 * 1024 * 1024;
    logging.RetainedFileCountLimit = 2;
    logging.FileName = "MyLogFile";
    logging.LogDirectory = @"logs";
    logging.FlushInterval = TimeSpan.FromSeconds(2);
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true); 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdPolicy", policy => policy.Requirements.Add(new UserIdRequirement()));
});
builder.Services.AddSingleton<IAuthorizationHandler, UserIdHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseW3CLogging();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapIdentityApi<User>();
app.MapBlazorHub();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using(var scope = scopeFactory.CreateScope())
{
    await AuthConfig.ConfigAdmin(scope.ServiceProvider);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapPost("/logout", async (SignInManager<User> signInManager,
    [FromBody] object empty) =>
{
    if (empty != null)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
    return Results.Unauthorized();
})
.WithOpenApi()
.RequireAuthorization();
app.MapRazorPages();

app.Run();
