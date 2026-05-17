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

        // SEARCH - AJAX autocomplete
        [HttpGet]
        public IActionResult Search(string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
            {
                return Json(new List<object>());
            }

            var query = q.ToLower();
            var customers = _repository.GetCustomers()
                .Where(c => c.FirstName.ToLower().Contains(query) || 
                           c.LastName.ToLower().Contains(query) ||
                           c.Email.ToLower().Contains(query))
                .Take(10)
                .Select(c => new { id = c.Id, label = $"{c.FirstName} {c.LastName} ({c.Email})", value = c.Id })
                .ToList();

            return Json(customers);
        }

            // API SEARCH - Index table search
            [HttpGet("[controller]/api/search")]
            public IActionResult ApiSearch(string q)
            {
                if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
                {
                    return PartialView("_SearchResults", new List<Customer>());
                }

                var query = q.ToLower();
                var customers = _repository.GetCustomers()
                    .Where(c => c.FirstName.ToLower().Contains(query) || 
                               c.LastName.ToLower().Contains(query) ||
                               c.Email.ToLower().Contains(query) ||
                               c.PhoneNumber.ToLower().Contains(query))
                    .ToList();

                return PartialView("_SearchResults", customers);
            }
    }
}
