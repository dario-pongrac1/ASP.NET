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
    }
}
