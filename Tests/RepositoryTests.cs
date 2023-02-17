using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.App.EF;
using Mapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests;

#pragma warning disable NUnit2005
class TestUserNameProvider : IUserNameProvider
{
    public string CurrentUserName { get; } = "-";
}

public class RepositoryTests
{
    private AppDbContext _ctx;
    private readonly AppUnitOfWork _uow;
    
    public RepositoryTests()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging();
        _ctx = new AppDbContext(optionsBuilder.Options, new TestUserNameProvider());
        _uow = new DAL.App.EF.AppUnitOfWork(_ctx);

        var locations = new List<Domain.App.Location>()
        {
            new()
            {
                Id = Guid.Parse("D82B3A89-082D-8E88-DE88-4F810EA141F9"),
                PlanetarySystemName = "Solar System",
                PlanetName = "Mercury",
                PlanetLocationName = "Delta Capricorn",
                UniquePlanetLocation3LetterIdentifier = "DCP"
            },
            new()
            {
                Id = Guid.Parse("84158C1C-7863-97FA-3D26-6FCFAB5EBCDE"),
                PlanetarySystemName = "Solar System",
                PlanetName = "Venus",
                PlanetLocationName = "Matar",
                UniquePlanetLocation3LetterIdentifier = "MAT"
            },
            new()
            {
                Id = Guid.Parse("5D4E1991-4CED-9D58-F76D-EE7E36A4923C"),
                PlanetarySystemName = "Solar System",
                PlanetName = "Earth",
                PlanetLocationName = "Zeta Aquilae",
                UniquePlanetLocation3LetterIdentifier = "ZAQ"
            },
            new()
            {
                Id = Guid.Parse("9BF64532-2E29-1C31-B661-46011911FB8A"),
                PlanetarySystemName = "Solar System",
                PlanetName = "Mars",
                PlanetLocationName = "Rutilicus",
                UniquePlanetLocation3LetterIdentifier = "RUT"
            },
            new()
            {
                Id = Guid.Parse("642CF2EB-29BE-785B-B038-6A2C85E7CA0E"),
                PlanetarySystemName = "Solar System",
                PlanetName = "Jupiter",
                PlanetLocationName = "Turais",
                UniquePlanetLocation3LetterIdentifier = "TUR"
            },
            new()
            {
                Id = Guid.Parse("D3B7D90F-28AB-D386-85A4-0214A2CBA6DA"),
                PlanetarySystemName = "Saturn",
                PlanetName = "Saturn",
                PlanetLocationName = "Algorel",
                UniquePlanetLocation3LetterIdentifier = "ALG"
            }
        };

        foreach (var location in locations)
        {
            _ctx.Location.Add(location);
        }
        var companies = new List<Domain.App.Company>()
        {
            new() {
                Id = Guid.Parse("D9AB4526-84CA-A0FA-86D7-5BEA48630AB4"),
                Name = "Spacegenix"
            },
            new() {
                Id = Guid.Parse("13DAEEF7-1290-3367-5903-A8CE66077B59"),
                Name = "Spacelux"
            },
            new() {
                Id = Guid.Parse("94E38B4A-8915-8791-0A95-EB9AFAA72A9F"),
                Name = "Space Odyssey"
            },
            new() {
                Id = Guid.Parse("24B08F4A-67C8-72D7-A102-A5BAEE171CD6"),
                Name = "Explore Origin"
            },
            new() {
                Id = Guid.Parse("124FF4AE-0A00-0042-897D-CF25EC9E386C"),
                Name = "SpaceX"
            },
            new() {
                Id = Guid.Parse("30B65028-4156-3DEC-5945-79286A0F5F03"),
                Name = "Space Piper"
            },
            new() {
                Id = Guid.Parse("5D04A0FA-358A-93C5-3348-2A80F02926AD"),
                Name = "Galaxy Express"
            },
            new() {
                Id = Guid.Parse("AB703C07-8320-5787-6807-B819BD73A232"),
                Name = "Travel Nova"
            },
            new() {
                Id = Guid.Parse("FFF0BB13-7E1F-7E66-7672-9DFE5F7060E6"),
                Name = "Explore Dynamite"
            },
            new() {
                Id = Guid.Parse("61395DA6-3CC7-6969-0E3F-15569BE4966A"),
                Name = "Space Voyager"
            },
        };
        foreach (var company in companies)
        {
            _ctx.Company.Add(company);
        }
        var customers = new List<Domain.App.Customer>()
        {
            new()
            {
                Id = Guid.Parse("336A2BC4-7283-4003-B71F-C651FDF2FF1D"),
                FirstName = "Will",
                LastName = "Smith",
            },
            new()
            {
                Id = Guid.Parse("B2897737-3173-4431-A071-B8B9EE25A3C7"),
                FirstName = "Elon",
                LastName = "Musk",
            }
        };
        foreach (var customer in customers)
        {
            _ctx.Customers.Add(customer);
        }
        var priceLists = new List<Domain.App.PriceList>()
        {
            new()
            {
                Id = Guid.Parse("B7DFC90C-DF66-42A9-9724-3B4FC794A561"),
                ValidUntil = DateTime.UtcNow.AddHours(-1),
                ValueJson = ""
            },
            new()
            {
                Id = Guid.Parse("CE3E3C7B-C95B-4148-B8CE-7E58E8CF1F64"),
                ValidUntil = DateTime.UtcNow,
                ValueJson = ""
            },
            new()
            {
                Id = Guid.Parse("031F5714-5045-48BB-A8B7-7F7839CA539F"),
                ValidUntil = DateTime.UtcNow.AddHours(1),
                ValueJson = ""
            }
        };
        foreach (var priceList in priceLists)
        {
            _ctx.PriceList.Add(priceList);
        }
        var providedRoutes = new List<Domain.App.ProvidedRoute>()
        {
            new()
            {
                Id = Guid.Parse("9CBFD744-7C85-4512-A35E-E07E1CB599DF"),
                PriceListId = priceLists[0].Id,
                PriceList = priceLists[0],
                FromLocationId = locations.FirstOrDefault(x => x.PlanetName == "Venus")!.Id,
                FromLocation = locations.FirstOrDefault(x => x.PlanetName == "Venus")!,
                DestinationLocationId = locations.FirstOrDefault(x => x.PlanetName == "Mars")!.Id,
                DestinationLocation = locations.FirstOrDefault(x => x.PlanetName == "Mars")!,
                Distance = 119740000,
                CompanyId = companies.FirstOrDefault(x => x.Name == "Spacegenix")!.Id,
                Company = companies.FirstOrDefault(x => x.Name == "Spacegenix")!,
                Price = 17000,
                FlightStart = DateTime.UtcNow.AddHours(41),
                FlightEnd = DateTime.UtcNow.AddHours(81),
            },
            new()
            {
                Id = Guid.Parse("9819BDB3-6DD9-4487-9C86-5EF0B4BFD80F"),
                PriceListId = priceLists[0].Id,
                PriceList = priceLists[0],
                FromLocationId = locations.FirstOrDefault(x => x.PlanetName == "Venus")!.Id,
                FromLocation = locations.FirstOrDefault(x => x.PlanetName == "Venus")!,
                DestinationLocationId = locations.FirstOrDefault(x => x.PlanetName == "Mars")!.Id,
                DestinationLocation = locations.FirstOrDefault(x => x.PlanetName == "Mars")!,
                Distance = 119740000,
                CompanyId = companies.FirstOrDefault(x => x.Name == "Spacelux")!.Id,
                Company = companies.FirstOrDefault(x => x.Name == "Spacelux")!,
                Price = 17000,
                FlightStart = DateTime.UtcNow.AddHours(41),
                FlightEnd = DateTime.UtcNow.AddHours(81),
            }
        };
        foreach (var providedRoute in providedRoutes)
        {
            _ctx.ProvidedRoute.Add(providedRoute);
        }
        

        _uow.SaveChanges();
    }
    

    [Test]
    public async Task CustomersRepo__GetCustomer_FirstOrDefaultAsync_WhereCustomerFirstNameEqualsArg1AndLastNameEqualsArg2__ReturnsCorrectCustomerOrNull()
    {
        var willSmith = await _uow.Customers.GetCustomer_FirstOrDefaultAsync_WhereCustomerFirstNameEqualsArg1AndLastNameEqualsArg2("Will", "Smith");
        var elonMusk = await _uow.Customers.GetCustomer_FirstOrDefaultAsync_WhereCustomerFirstNameEqualsArg1AndLastNameEqualsArg2("Elon", "Musk");
        var elonMuskLowercase = await _uow.Customers.GetCustomer_FirstOrDefaultAsync_WhereCustomerFirstNameEqualsArg1AndLastNameEqualsArg2("elon", "musk");
        
        Assert.NotNull(willSmith);
        Assert.NotNull(elonMusk);
        Assert.NotNull(elonMuskLowercase);
        Assert.AreEqual( "Will", willSmith.FirstName);
        Assert.AreEqual( "Smith", willSmith.LastName);
        Assert.AreEqual("Elon", elonMusk.FirstName);
        Assert.AreEqual("Musk", elonMusk.LastName);
        Assert.IsNull(elonMuskLowercase);
    }
    
    [Test]
    public async Task ProvidedRoutesRepo__ProvidedRoutes_IncludeLocation_WhereFromLocationIdEqualsArg_SelectDestinationLocation_Distinct_ToListAsync__ReturnsDistinctResult()
    {
        var venusId = Guid.Parse("84158C1C-7863-97FA-3D26-6FCFAB5EBCDE");
        var providedRoutes = await _uow.ProvidedRoutes.ProvidedRoutes_IncludeLocation_WhereFromLocationIdEqualsArg_SelectDestinationLocation_Distinct_ToListAsync(venusId);

        Assert.NotNull(providedRoutes);
        Assert.AreEqual(1, providedRoutes.Count);
        Assert.AreEqual("Mars", providedRoutes[0].PlanetName);

    }
    
    [Test]
    public async Task ProvidedRoutesRepo__OrderByValidUntilDescendingThenSkipNThenDeleteAll__DeletesCorrectEntity()
    {
        var priceListsBefore = await _uow.PriceLists.GetAllAsyncBase();
        var affectedRowNumber = await _uow.PriceLists.OrderByValidUntilDescendingThenSkipNThenDeleteAll(2);
        var priceListsAfter = await _uow.PriceLists.GetAllAsyncBase();
        
        Assert.AreEqual(3, priceListsBefore.Count);
        Assert.AreEqual(1, affectedRowNumber);
        Assert.True(priceListsAfter.Select(x => x.Id).ToList().Contains(Guid.Parse("CE3E3C7B-C95B-4148-B8CE-7E58E8CF1F64")));
        Assert.True(priceListsAfter.Select(x => x.Id).ToList().Contains(Guid.Parse("031F5714-5045-48BB-A8B7-7F7839CA539F")));

    }
}
