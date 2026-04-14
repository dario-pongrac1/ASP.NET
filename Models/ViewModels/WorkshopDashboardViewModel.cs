using lab_1.Models.Entities;

namespace lab_1.Models.ViewModels
{
    public class WorkshopDashboardViewModel
    {
        public int CustomerCount { get; set; }
        public int VehicleCount { get; set; }
        public int ServiceOrderCount { get; set; }
        public int MechanicCount { get; set; }
        public int ActiveOrderCount { get; set; }
        public IReadOnlyList<AppointmentSlot> UpcomingSlots { get; set; } = [];
    }
}
