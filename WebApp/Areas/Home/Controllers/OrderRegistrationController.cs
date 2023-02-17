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
        if (! ModelState.IsValid)
        {
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
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
        if (! ModelState.IsValid)
        {
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
        var providedRoutes = await _uow.ProvidedRoutes.ProvidedRoutes_GetAll_WhereFromLocationIdEqualsArg1AndToLocationIdEqualsArg2_ToListAsync(from, to);
        return View(providedRoutes);
    }

    [HttpGet]
    public async Task<IActionResult> CreateOrder(Guid id)
    {
        if (! ModelState.IsValid)
        {
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
        var providedRoute = await _uow.ProvidedRoutes.FirstOrDefault(id);
        if (providedRoute == null || providedRoute.PriceList.ValidUntil < DateTime.UtcNow)
        {
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
        var createOrderModel = new WebDTO.CreateOrder()
        {
            FirstName = "",
            LastName = "",
            ProvidedRouteId = providedRoute.Id,
            ProvidedRoute = providedRoute
        };
        return View(createOrderModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(Guid providedRouteId, string firstName, string lastName)
    {
        if (! ModelState.IsValid)
        {
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
        var providedRoute = await _uow.ProvidedRoutes.FirstOrDefault(providedRouteId);
        if (providedRoute == null || providedRoute.PriceList.ValidUntil < DateTime.UtcNow)
        {
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
        var orderId = Guid.NewGuid();
        var customer = await _uow.Customers.GetCustomer_FirstOrDefaultAsync_WhereCustomerFirstNameEqualsArg1AndLastNameEqualsArg2(firstName, lastName);
        if (customer == null)
        {
            var newCustomer = new DAL.App.DTO.Customer
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Orders = new List<Order>(),
            };
            customer = await _uow.Customers.Add(newCustomer);
        }
        var order = new DAL.App.DTO.Order()
        {
            Id = orderId,
            DateOfPurchase = DateTime.UtcNow,
            CompanyName = providedRoute.Company.Name,
            FlightStart = providedRoute.FlightStart,
            FlightEnd = providedRoute.FlightEnd,
            Price = providedRoute.Price,
            RouteName = $"{providedRoute.FromLocation.PlanetName} -> {providedRoute.DestinationLocation.PlanetName}",
            CustomerId = customer.Id,
        };
        await _uow.Orders.Add(order);
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(OrderController.ShowCustomerOrders), "Order", new { id = customer.Id, created = true });
    }
}