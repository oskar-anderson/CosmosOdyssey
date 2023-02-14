using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class LocationController : Controller
{
    private readonly AppUnitOfWork _uow;
    
    public LocationController(DAL.App.EF.AppDbContext context)
    {
        _uow = new DAL.App.EF.AppUnitOfWork(context);
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var locations = await _uow.Locations.GetAllAsyncBase();
        return View(locations);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(Guid? id)
    {
        // For create
        Console.WriteLine($"id: {id}");
        if (id == null)
        {
            var newLocation = new DAL.App.DTO.Location()
            {
                Id = Guid.NewGuid(), 
                PlanetarySystemName = "",
                PlanetName = "",
                PlanetLocationName = "",
                UniquePlanetLocation3LetterIdentifier = ""
            };
            return GetDetailsView(newLocation, true);
        }
        // For delete/details
        var location = await _uow.Locations.FirstOrDefault(id.Value);
        if (location == null) { return NotFound(); }
        return GetDetailsView(location, false);
    }

    public ViewResult GetDetailsView(DAL.App.DTO.Location location, bool isCreate)
    {
        if (isCreate)
        {
            ViewData["btn-type"] = "btn-primary";
            ViewData["actionControllerMethodNameDeleteOrCreate"] = nameof(Create);
            ViewData["actionName"] = "Create";
            return View("Details", location);
        }
        ViewData["btn-type"] = "btn-danger";
        ViewData["actionControllerMethodNameDeleteOrCreate"] = nameof(Delete);
        ViewData["actionName"] = "Delete";
        return View("Details", location);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var location = await _uow.Locations.FirstOrDefault(id);
        if (location == null)
        {
            return NotFound();
        }
        await _uow.Locations.RemoveAsync(location);
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Create(DAL.App.DTO.Location location)
    {
        if (!ModelState.IsValid) return GetDetailsView(location, true);
        await _uow.Locations.Add(location);
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}