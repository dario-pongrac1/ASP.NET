using lab_1.Models.Entities;
using lab_1.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lab_1.Controllers
{
    public class ServiceOrdersController : Controller
    {
        private readonly IWorkshopReadRepository _repository;

        public ServiceOrdersController(IWorkshopReadRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var serviceOrders = _repository.GetServiceOrders();
            return View(serviceOrders);
        }

        public IActionResult Details(int id)
        {
            var serviceOrder = _repository.GetServiceOrderById(id);
            if (serviceOrder is null)
            {
                return NotFound();
            }

            return View(serviceOrder);
        }

        public IActionResult Create()
        {
            LoadDropDowns();
            return View(new ServiceOrder());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceOrder model)
        {
            if (ModelState.IsValid)
            {
                _repository.AddServiceOrder(model);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropDowns();
            return View(model);
        }

        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            var serviceOrder = _repository.GetServiceOrderById(id);
            if (serviceOrder is null)
            {
                return NotFound();
            }

            LoadDropDowns();
            return View(serviceOrder);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var serviceOrder = _repository.GetServiceOrderById(id);
            if (serviceOrder is null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(serviceOrder))
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateServiceOrder(serviceOrder);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            LoadDropDowns();
            return View("Edit", serviceOrder);
        }

        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            var serviceOrder = _repository.GetServiceOrderById(id);
            if (serviceOrder is null)
            {
                return NotFound();
            }

            return View(serviceOrder);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var serviceOrder = _repository.GetServiceOrderById(id);
            if (serviceOrder is null)
            {
                return NotFound();
            }

            _repository.DeleteServiceOrder(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void LoadDropDowns()
        {
            ViewBag.Customers = _repository.GetCustomers();
            ViewBag.Vehicles = _repository.GetVehicles();
            ViewBag.Mechanics = _repository.GetMechanics();
        }

        [HttpGet]
        public IActionResult Search(string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
            {
                return Json(new List<object>());
            }

            var query = q.ToLower();
            var orders = _repository.GetServiceOrders()
                .Where(o => o.OrderNumber.ToLower().Contains(query))
                .Take(10)
                .Select(o => new { id = o.Id, label = $"{o.OrderNumber} ({o.Status})", value = o.Id })
                .ToList();

            return Json(orders);
        }

        [HttpGet("[controller]/api/search")]
        public IActionResult ApiSearch(string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
            {
                return PartialView("_SearchResults", new List<ServiceOrder>());
            }

            var query = q.ToLower();
            var orders = _repository.GetServiceOrders()
                .Where(o => o.OrderNumber.ToLower().Contains(query) ||
                           o.Notes.ToLower().Contains(query) ||
                           (o.Customer != null && (
                               o.Customer.FirstName.ToLower().Contains(query) ||
                               o.Customer.LastName.ToLower().Contains(query))) ||
                           (o.Vehicle != null && (
                               o.Vehicle.LicensePlate.ToLower().Contains(query) ||
                               o.Vehicle.Manufacturer.ToLower().Contains(query) ||
                               o.Vehicle.Model.ToLower().Contains(query))) ||
                           (o.Mechanic != null && (
                               o.Mechanic.FirstName.ToLower().Contains(query) ||
                               o.Mechanic.LastName.ToLower().Contains(query))))
                .Take(10)
                .ToList();

            return PartialView("_SearchResults", orders);
        }
    }
}
