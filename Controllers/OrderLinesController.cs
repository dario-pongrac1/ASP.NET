using lab_1.Models.Entities;
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

        public IActionResult Create()
        {
            LoadDropDowns();
            return View(new OrderLine());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderLine model)
        {
            if (ModelState.IsValid)
            {
                _repository.AddOrderLine(model);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropDowns();
            return View(model);
        }

        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            var orderLine = _repository.GetOrderLineById(id);
            if (orderLine is null)
            {
                return NotFound();
            }

            LoadDropDowns();
            return View(orderLine);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            var orderLine = _repository.GetOrderLineById(id);
            if (orderLine is null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(orderLine))
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateOrderLine(orderLine);
                    await _repository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            LoadDropDowns();
            return View("Edit", orderLine);
        }

        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            var orderLine = _repository.GetOrderLineById(id);
            if (orderLine is null)
            {
                return NotFound();
            }

            return View(orderLine);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var orderLine = _repository.GetOrderLineById(id);
            if (orderLine is null)
            {
                return NotFound();
            }

            _repository.DeleteOrderLine(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void LoadDropDowns()
        {
            ViewBag.ServiceOrders = _repository.GetServiceOrders();
            ViewBag.ServiceItems = _repository.GetServiceItems();
        }
    }
}
