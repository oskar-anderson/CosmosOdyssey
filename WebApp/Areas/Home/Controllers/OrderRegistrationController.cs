using DAL.App.DTO;
using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Views.Controllers;

namespace WebApp.Areas.Home.Controllers;

[Area("Home")]
public class OrderRegistrationController : Controller
{
    private readonly AppUnitOfWork _uow;
    
    public OrderRegistrationController(DAL.App.EF.AppDbContext context)
    {
        _uow = new DAL.App.EF.AppUnitOfWork(context);
    }
    
    [HttpGet]
    public async Task<IActionResult> PlanetForm1()
    {
        var model = new WebDTO.OrderRegistration1()
        {
            ErrorMsg = null,
            FromPlanets = new SelectList(await _uow.Locations.GetAllAsyncBase(), nameof(DAL.App.DTO.Location.Id), nameof(DAL.App.DTO.Location.PlanetName))
        };

        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> PlanetForm2(Guid from)
    {
        var locations = await _uow.ProvidedRoutes.ProvidedRoutes_IncludeLocation_WhereFromLocationIdEqualsArg_SelectDestinationLocation_Distinct_ToListAsync(from);
        if (locations.Count == 0)
        {
            var planet = await _uow.Locations.FirstOrDefault(from);
            var form1Model = new WebDTO.OrderRegistration1()
            {
                ErrorMsg = planet == null ? "Error getting flight!" : $"No flights from {planet.PlanetName}",
                FromPlanets = new SelectList(await _uow.Locations.GetAllAsyncBase(), nameof(DAL.App.DTO.Location.Id),
                    nameof(DAL.App.DTO.Location.PlanetName))
            };
            return View("PlanetForm1", form1Model);
        }
        var form2Model = new WebDTO.OrderRegistration2()
        {
            From = from,
            ToPlanets = new SelectList(locations, nameof(DAL.App.DTO.Location.Id), nameof(DAL.App.DTO.Location.PlanetName))
        };
        return View(form2Model);
    }
    
    [HttpGet]
    public async Task<IActionResult> AvailableFlights(Guid from, Guid to)
    {
        var providedRoutes = await _uow.ProvidedRoutes.ProvidedRoutes_GetAll_WhereFromLocationIdEqualsArg1AndToLocationIdEqualsArg2_ToListAsync(from, to);
        return View(providedRoutes);
    }

    [HttpGet]
    public async Task<IActionResult> CreateOrder(Guid id)
    {
        var providedRoute = await _uow.ProvidedRoutes.FirstOrDefault(id) ?? throw new ArgumentException($"System error, no providedRoutes with id: {id}");
        var createOrderModel = new WebDTO.CreateOrder()
        {
            FirstName = "",
            LastName = "",
            ProvidedRoute = providedRoute
        };
        return View(createOrderModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(Guid providerId, string firstName, string lastName)
    {
        var provider = await _uow.ProvidedRoutes.FirstOrDefault(providerId) ?? throw new ArgumentException($"System error, no providedRoutes with id: {providerId}");
        var orderId = Guid.NewGuid();
        var order = new DAL.App.DTO.Order()
        {
            Id = orderId,
            DateOfPurchase = DateTime.UtcNow,
            FirstName = firstName,
            LastName = lastName,
            OrderLines = new List<OrderLine>() { new()
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    CompanyName = provider.Company.Name,
                    FlightStart = provider.FlightStart,
                    FlightEnd = provider.FlightEnd,
                    Price = provider.Price,
                    RouteName = $"{provider.FromLocation.PlanetName} -> {provider.DestinationLocation.PlanetName}",
                }
            }
        };
        await _uow.Orders.Add(order);
        return RedirectToAction("Index", "Home");
    }
}