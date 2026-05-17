using lab_1.Models.Entities;
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

        public IActionResult Create()
        {
            LoadDropDowns();
            return View(new AppointmentSlot());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentSlot model)
        {
            if (ModelState.IsValid)
            {
                _repository.AddAppointmentSlot(model);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropDowns();
            return View(model);
        }

        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            var slot = _repository.GetAppointmentSlotById(id);
            if (slot is null)
            {
                return NotFound();
            }

            LoadDropDowns();
            return View(slot);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var slot = _repository.GetAppointmentSlotById(id);
            if (slot is null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(slot))
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateAppointmentSlot(slot);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            LoadDropDowns();
            return View("Edit", slot);
        }

        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            var slot = _repository.GetAppointmentSlotById(id);
            if (slot is null)
            {
                return NotFound();
            }

            return View(slot);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var slot = _repository.GetAppointmentSlotById(id);
            if (slot is null)
            {
                return NotFound();
            }

            _repository.DeleteAppointmentSlot(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void LoadDropDowns()
        {
            ViewBag.Mechanics = _repository.GetMechanics();
            ViewBag.ServiceOrders = _repository.GetServiceOrders();
        }

        [HttpGet("[controller]/api/search")]
        public IActionResult ApiSearch(string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
            {
                return PartialView("_SearchResults", new List<AppointmentSlot>());
            }

            var query = q.ToLower();
            var slots = _repository.GetAppointmentSlots()
                .Where(slot => slot.StartAt.ToString("dd.MM.yyyy HH:mm").ToLower().Contains(query) ||
                               slot.EndAt.ToString("dd.MM.yyyy HH:mm").ToLower().Contains(query) ||
                               (slot.Mechanic != null && (
                                   slot.Mechanic.FirstName.ToLower().Contains(query) ||
                                   slot.Mechanic.LastName.ToLower().Contains(query))) ||
                               (slot.ServiceOrder != null && slot.ServiceOrder.OrderNumber.ToLower().Contains(query)))
                .Take(10)
                .ToList();

            return PartialView("_SearchResults", slots);
        }
    }
}
