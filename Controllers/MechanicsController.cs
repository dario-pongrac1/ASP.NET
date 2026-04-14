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
    }
}
