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
                .UseLoggerFactory(loggerFactory);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        // makes httpcontext injectable - needed to resolve username in dal layer
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IUserNameProvider, UserNameProvider>();
        
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services
            .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AppDbContext>();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

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
