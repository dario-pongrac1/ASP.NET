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
    }
}
