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
    }
}
