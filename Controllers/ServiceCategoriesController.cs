using lab_1.Models.Entities;
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

        public IActionResult Create()
        {
            return View(new ServiceCategory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCategory model)
        {
            if (ModelState.IsValid)
            {
                _repository.AddServiceCategory(model);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            var category = _repository.GetServiceCategoryById(id);
            if (category is null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var category = _repository.GetServiceCategoryById(id);
            if (category is null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(category))
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateServiceCategory(category);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View("Edit", category);
        }

        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            var category = _repository.GetServiceCategoryById(id);
            if (category is null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var category = _repository.GetServiceCategoryById(id);
            if (category is null)
            {
                return NotFound();
            }

            _repository.DeleteServiceCategory(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Search(string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
            {
                return Json(new List<object>());
            }

            var query = q.ToLower();
            var categories = _repository.GetServiceCategories()
                .Where(c => c.Name.ToLower().Contains(query) || 
                           c.Description.ToLower().Contains(query))
                .Take(10)
                .Select(c => new { id = c.Id, label = c.Name, value = c.Id })
                .ToList();

            return Json(categories);
        }

        [HttpGet("[controller]/api/search")]
        public IActionResult ApiSearch(string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
            {
                return PartialView("_SearchResults", new List<ServiceCategory>());
            }

            var query = q.ToLower();
            var categories = _repository.GetServiceCategories()
                .Where(c => c.Name.ToLower().Contains(query) ||
                           c.Description.ToLower().Contains(query))
                .Take(10)
                .ToList();

            return PartialView("_SearchResults", categories);
        }
    }
}
