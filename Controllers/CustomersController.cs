using lab_1.Services.Repositories;
using lab_1.Models.Entities;
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

        // CREATE - GET
        public IActionResult Create()
        {
            var model = new Customer();
            return View(model);
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer model)
        {
            if (ModelState.IsValid)
            {
                _repository.AddCustomer(model);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // EDIT - GET
        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            var customer = _repository.GetCustomerById(id);
            if (customer is null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // EDIT - POST
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var customer = _repository.GetCustomerById(id);
            if (customer is null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(customer))
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateCustomer(customer);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View("Edit", customer);
        }

        // DELETE - GET (potvrda)
        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            var customer = _repository.GetCustomerById(id);
            if (customer is null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // DELETE - POST (izvršavanje)
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var customer = _repository.GetCustomerById(id);
            if (customer is null)
            {
                return NotFound();
            }

            _repository.DeleteCustomer(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
