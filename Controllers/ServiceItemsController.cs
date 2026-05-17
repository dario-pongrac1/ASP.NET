using lab_1.Models.Entities;
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

        public IActionResult Create()
        {
            ViewBag.ServiceCategories = _repository.GetServiceCategories();
            return View(new ServiceItem());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceItem model)
        {
            if (ModelState.IsValid)
            {
                _repository.AddServiceItem(model);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ServiceCategories = _repository.GetServiceCategories();
            return View(model);
        }

        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            var serviceItem = _repository.GetServiceItemById(id);
            if (serviceItem is null)
            {
                return NotFound();
            }

            ViewBag.ServiceCategories = _repository.GetServiceCategories();
            return View(serviceItem);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var serviceItem = _repository.GetServiceItemById(id);
            if (serviceItem is null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(serviceItem))
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateServiceItem(serviceItem);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.ServiceCategories = _repository.GetServiceCategories();
            return View("Edit", serviceItem);
        }

        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            var serviceItem = _repository.GetServiceItemById(id);
            if (serviceItem is null)
            {
                return NotFound();
            }

            return View(serviceItem);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var serviceItem = _repository.GetServiceItemById(id);
            if (serviceItem is null)
            {
                return NotFound();
            }

            _repository.DeleteServiceItem(id);
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
            var items = _repository.GetServiceItems()
                .Where(i => i.Name.ToLower().Contains(query) || 
                           i.Description.ToLower().Contains(query))
                .Take(10)
                .Select(i => new { id = i.Id, label = $"{i.Name} ({i.BasePrice:C})", value = i.Id })
                .ToList();

            return Json(items);
        }

        [HttpGet("[controller]/api/search")]
        public IActionResult ApiSearch(string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
            {
                return PartialView("_SearchResults", new List<ServiceItem>());
            }

            var query = q.ToLower();
            var items = _repository.GetServiceItems()
                .Where(i => i.Name.ToLower().Contains(query) ||
                           i.Description.ToLower().Contains(query) ||
                           (i.Category != null && i.Category.Name.ToLower().Contains(query)))
                .Take(10)
                .ToList();

            return PartialView("_SearchResults", items);
        }
    }
}
