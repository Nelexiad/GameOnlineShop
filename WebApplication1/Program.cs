using Entities.Data;
using Entities.Models;
using Entities.Models.DTO;
using Game_ECommerce.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Repositories.Repositories;
using Serilog;
using Services;
using WebApplication1.Controllers;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//builder.Services.AddDbContextFactory<ApplicationDbContext>();
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(VideogameProfile));
builder.Services.AddScoped<IDbContext<ApplicationDbContext>, ApplicationDbContext>();
//builder.Services.AddLogging(builder =>
//{
//    builder.AddConsole();
//    builder.AddDebug();

//});

builder.Host.UseSerilog((hostingContext, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(hostingContext.Configuration);
});
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddUserManager<CustomUserManager>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<BaseRepositories<Videogame, VideogameDTO>, VideogameRepository>();
builder.Services.AddScoped<VideogameRepository>();
builder.Services.AddScoped<VideogameServices>();
builder.Services.AddHttpClient(VideogameServices.ClientName,
    client =>
    {
        client.BaseAddress = new Uri("https://localhost:7115/api/");
    }).AddTransientHttpErrorPolicy(policyBuilde => policyBuilde.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));



var app = builder.Build();

//using(var scope = app.Services.CreateScope())
//{
//   await DbSeeder.SeedDafaultData(scope.ServiceProvider);
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
