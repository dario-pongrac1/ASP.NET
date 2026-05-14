using lab_1.Models.Entities;
using lab_1.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lab_1.Controllers
{
    public class MechanicsController : Controller
    {
        private readonly IWorkshopReadRepository _repository;

        public MechanicsController(IWorkshopReadRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var mechanics = _repository.GetMechanics();
            return View(mechanics);
        }

        public IActionResult Details(int id)
        {
            var mechanic = _repository.GetMechanicById(id);
            if (mechanic is null)
            {
                return NotFound();
            }

            return View(mechanic);
        }

        public IActionResult Create()
        {
            return View(new Mechanic());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mechanic model)
        {
            if (ModelState.IsValid)
            {
                _repository.AddMechanic(model);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            var mechanic = _repository.GetMechanicById(id);
            if (mechanic is null)
            {
                return NotFound();
            }

            return View(mechanic);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var mechanic = _repository.GetMechanicById(id);
            if (mechanic is null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(mechanic))
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateMechanic(mechanic);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View("Edit", mechanic);
        }

        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            var mechanic = _repository.GetMechanicById(id);
            if (mechanic is null)
            {
                return NotFound();
            }

            return View(mechanic);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var mechanic = _repository.GetMechanicById(id);
            if (mechanic is null)
            {
                return NotFound();
            }

            _repository.DeleteMechanic(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
