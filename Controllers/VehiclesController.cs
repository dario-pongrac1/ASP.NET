using lab_1.Models.Entities;
using lab_1.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lab_1.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IWorkshopReadRepository _repository;

        public VehiclesController(IWorkshopReadRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var vehicles = _repository.GetVehicles();
            return View(vehicles);
        }

        public IActionResult Details(int id)
        {
            var vehicle = _repository.GetVehicleById(id);
            if (vehicle is null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = _repository.GetCustomers();
            return View(new Vehicle());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _repository.AddVehicle(vehicle);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = _repository.GetCustomers();
            return View(vehicle);
        }

        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            var vehicle = _repository.GetVehicleById(id);
            if (vehicle is null)
            {
                return NotFound();
            }

            ViewBag.Customers = _repository.GetCustomers();
            return View(vehicle);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var vehicle = _repository.GetVehicleById(id);
            if (vehicle is null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(vehicle))
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateVehicle(vehicle);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.Customers = _repository.GetCustomers();
            return View("Edit", vehicle);
        }

        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            var vehicle = _repository.GetVehicleById(id);
            if (vehicle is null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var vehicle = _repository.GetVehicleById(id);
            if (vehicle is null)
            {
                return NotFound();
            }

            _repository.DeleteVehicle(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
