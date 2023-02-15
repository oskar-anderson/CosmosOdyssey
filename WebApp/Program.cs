using Contracts.DAL.Base;
using DAL.App.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;
using WebApp.Services;

namespace WebApp;

class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // PostgreSQL Datetime support
        builder.Configuration.AddJsonFile("appsettings.secret.json", optional: false, reloadOnChange: true);  // will override appsettings.json keys
        
        // Add logging
        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(c => 
        {
            c.TimestampFormat = "[HH:mm:ss] ";
        });
        
        // Add services to the container.
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                                   throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            options
                .UseNpgsql(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        })
            .AddHttpContextAccessor()         // makes httpcontext injectable - needed to resolve username in dal layer
            .AddScoped<IUserNameProvider, UserNameProvider>()
            .AddDatabaseDeveloperPageExceptionFilter()
            .AddControllersWithViews();

        // add identity
        builder.Services
            .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AppDbContext>();
        
        // register timer based data querying services
        builder.Services.AddHttpClient<IGetApiTravelPrices, GetApiTravelPrices>(); // An HttpClient can be requested in IGetApiTravelPrices implementation using dependency injection (DI)
        builder.Services.AddSingleton<IGetApiTravelPrices, GetApiTravelPrices>();
        
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

        UpdateDatabase(app, builder.Environment, builder.Configuration);
        
        using var scope = (app.Services.GetService<IServiceScopeFactory>() ?? throw new InvalidOperationException("IServiceScopeFactory could not be created.")).CreateScope();
        var travelPricesApi = scope.ServiceProvider.GetRequiredService<IGetApiTravelPrices>();
        travelPricesApi.Start(app.Configuration.GetValue<string>("ApiGetTravelPrices") ?? throw new InvalidOperationException("Setting 'ApiGetTravelPrices' not found."));
        app.Run();
    }

    private static void UpdateDatabase(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        // give me the scoped services (everything created by it will be closed at the end of service scope life).
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>() ?? throw new Exception("Cannot create AppDbContext!");
        
        if (configuration.GetValue<bool>("AppDataInitialization:DropDatabase"))
        {
            Console.WriteLine("DropDatabase");
            ctx.Database.EnsureDeleted();
        }
            
        if (configuration.GetValue<bool>("AppDataInitialization:MigrateDatabase"))
        {
            Console.WriteLine("MigrateDatabase");
            ctx.Database.Migrate();
        }

        if (configuration.GetValue<bool>("AppDataInitialization:SeedData"))
        {
            Console.WriteLine("SeedData");
            var companies = new DAL.App.EF.Helpers.DataInitializer().ReadCompanies(System.AppDomain.CurrentDomain.BaseDirectory + "Data/libre-companies.csv");
            var locations = new DAL.App.EF.Helpers.DataInitializer().ReadLocations(System.AppDomain.CurrentDomain.BaseDirectory + "Data/libre-locations.csv");
            ctx.Company.AddRange(companies.Select(x => new Mapper.CompanyMapper().DalToDomain(x)).ToList());
            ctx.Location.AddRange(locations.Select(x => new Mapper.LocationMapper().DalToDomain(x)).ToList());
            ctx.SaveChanges();
        }
    }
}
