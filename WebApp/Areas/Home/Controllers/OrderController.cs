using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using WebApp.Views.Controllers;

namespace WebApp.Areas.Home.Controllers;

[Area("Home")]
public class OrderController : Controller
{
    private readonly AppUnitOfWork _uow;
    
    public OrderController(DAL.App.EF.AppDbContext context)
    {
        _uow = new DAL.App.EF.AppUnitOfWork(context);
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCustomerByName(string firstName, string lastName)
    {
        if (! ModelState.IsValid)
        {
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }
        var customer = await _uow.Customers.GetCustomer_FirstOrDefaultAsync_WhereCustomerFirstNameEqualsArg1AndLastNameEqualsArg2(firstName, lastName);
        if (customer == null)
        {
            ViewData["errorMsg"] = $"No customer with first name: {firstName} and last name: {lastName} found!";
            return View("Index");
        }
        var orders = await _uow.Orders.GetOrdersWithCustomer_WhereCustomerIdEqualsArg(customer.Id);
        return RedirectToAction("ShowCustomerOrders", new { id = customer.Id, created = false } );
    }
    
    [HttpGet]
    public async Task<IActionResult> ShowCustomerOrders(Guid id, bool created)
    {
        if (! ModelState.IsValid)
        {
            return RedirectToAction(nameof(HomeController.Error), "Home");
        }

        if (created)
        {
            ViewData["successMsg"] = "Order created!";
        }

        var orders = await _uow.Orders.GetOrdersWithCustomer_WhereCustomerIdEqualsArg(id);
        return View("ShowCustomerOrders", orders);
    }
}