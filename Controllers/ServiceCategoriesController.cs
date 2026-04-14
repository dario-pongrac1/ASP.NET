using lab_1.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lab_1.Controllers
{
    public class ServiceCategoriesController : Controller
    {
        private readonly IWorkshopReadRepository _repository;

        public ServiceCategoriesController(IWorkshopReadRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var categories = _repository.GetServiceCategories();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = _repository.GetServiceCategoryById(id);
            if (category is null)
            {
                return NotFound();
            }

            return View(category);
        }
    }
}
