using Contracts.DAL.Base;
using DAL.App.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;

namespace WebApp;

class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // PostgreSQL Datetime support
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        builder.Configuration.AddJsonFile("appsettings.secret.json", optional: false, reloadOnChange: true);  // will override appsettings.json keys
        Console.WriteLine(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
        // Add services to the container.
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            var loggerFactory = LoggerFactory.Create(
                _builder => 
                { 
                    _builder.AddSimpleConsole(c => 
                    {
                        c.TimestampFormat = "[HH:mm:ss] ";
                    }); 
                });
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                                   throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            options
                .UseNpgsql(connectionString)
                .UseLoggerFactory(loggerFactory)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        })
            .AddHttpContextAccessor()         // makes httpcontext injectable - needed to resolve username in dal layer
            .AddScoped<IUserNameProvider, UserNameProvider>()
            .AddDatabaseDeveloperPageExceptionFilter()
            .AddControllersWithViews();

        builder.Services
            .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AppDbContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
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
            name: "area",
            pattern: "{area:exists=Home}/{controller=Home}/{action=Index}/{id?}"
        );
        
        app.Run();
    }
}
