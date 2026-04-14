using lab_1.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lab_1.Controllers
{
    public class ServiceItemsController : Controller
    {
        private readonly IWorkshopReadRepository _repository;

        public ServiceItemsController(IWorkshopReadRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var serviceItems = _repository.GetServiceItems();
            return View(serviceItems);
        }

        public IActionResult Details(int id)
        {
            var serviceItem = _repository.GetServiceItemById(id);
            if (serviceItem is null)
            {
                return NotFound();
            }

            return View(serviceItem);
        }
    }
}
