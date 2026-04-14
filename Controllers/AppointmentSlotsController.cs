using lab_1.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lab_1.Controllers
{
    public class AppointmentSlotsController : Controller
    {
        private readonly IWorkshopReadRepository _repository;

        public AppointmentSlotsController(IWorkshopReadRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var slots = _repository.GetAppointmentSlots();
            return View(slots);
        }

        public IActionResult Details(int id)
        {
            var slot = _repository.GetAppointmentSlotById(id);
            if (slot is null)
            {
                return NotFound();
            }

            return View(slot);
        }
    }
}
