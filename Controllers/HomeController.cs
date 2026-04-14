using Microsoft.AspNetCore.Mvc;
using lab_1.Models.Enums;
using lab_1.Models.ViewModels;
using lab_1.Services.Repositories;

namespace lab_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkshopReadRepository _repository;

        public HomeController(IWorkshopReadRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var orders = _repository.GetServiceOrders();
            var model = new WorkshopDashboardViewModel
            {
                CustomerCount = _repository.GetCustomers().Count,
                VehicleCount = _repository.GetVehicles().Count,
                ServiceOrderCount = orders.Count,
                MechanicCount = _repository.GetMechanics().Count,
                ActiveOrderCount = orders.Count(o => o.Status is OrderStatus.Scheduled or OrderStatus.InProgress),
                UpcomingSlots = _repository.GetAppointmentSlots()
                    .OrderBy(slot => slot.StartAt)
                    .Take(5)
                    .ToList()
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Problem("Doslo je do neocekivane greske.");
        }
    }
}
