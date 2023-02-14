using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class CompanyController : Controller
{
    private readonly AppUnitOfWork _uow;
    
    public CompanyController(DAL.App.EF.AppDbContext context)
    {
        _uow = new DAL.App.EF.AppUnitOfWork(context);
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var companies = await _uow.Companies.GetAllAsyncBase();
        return View(companies);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(Guid? id)
    {
        // For create
        Console.WriteLine($"id: {id}");
        if (id == null)
        {
            var newCompany = new DAL.App.DTO.Company() {Id = Guid.NewGuid(), Name = ""};
            return GetDetailsView(newCompany, true);
        }
        // For delete/details
        var company = await _uow.Companies.FirstOrDefault(id.Value);
        if (company == null)
        {
            return NotFound();
        }
        return GetDetailsView(company, false);
    }

    public ViewResult GetDetailsView(DAL.App.DTO.Company company, bool isCreate)
    {
        if (isCreate)
        {
            ViewData["btn-type"] = "btn-primary";
            ViewData["actionControllerMethodNameDeleteOrCreate"] = nameof(Create);
            ViewData["actionName"] = "Create";
            return View("Details", company);
        }
        ViewData["btn-type"] = "btn-danger";
        ViewData["actionControllerMethodNameDeleteOrCreate"] = nameof(Delete);
        ViewData["actionName"] = "Delete";
        return View("Details", company);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var company = await _uow.Companies.FirstOrDefault(id);
        if (company == null)
        {
            return NotFound();
        }
        await _uow.Companies.RemoveAsync(company);
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Create(DAL.App.DTO.Company company)
    {
        Console.WriteLine($"company: {System.Text.Json.JsonSerializer.Serialize(company)}");
        if (!ModelState.IsValid) return GetDetailsView(company, true);
        await _uow.Companies.Add(company);
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}