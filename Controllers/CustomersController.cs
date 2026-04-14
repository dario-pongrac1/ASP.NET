using lab_1.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lab_1.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IWorkshopReadRepository _repository;

        public CustomersController(IWorkshopReadRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var customers = _repository.GetCustomers();
            return View(customers);
        }

        public IActionResult Details(int id)
        {
            var customer = _repository.GetCustomerById(id);
            if (customer is null)
            {
                return NotFound();
            }

            return View(customer);
        }
    }
}
