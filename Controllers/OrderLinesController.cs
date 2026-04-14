using lab_1.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lab_1.Controllers
{
    public class OrderLinesController : Controller
    {
        private readonly IWorkshopReadRepository _repository;

        public OrderLinesController(IWorkshopReadRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var orderLines = _repository.GetOrderLines();
            return View(orderLines);
        }

        public IActionResult Details(int id)
        {
            var orderLine = _repository.GetOrderLineById(id);
            if (orderLine is null)
            {
                return NotFound();
            }

            return View(orderLine);
        }
    }
}
