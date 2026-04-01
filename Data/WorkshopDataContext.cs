using System.Collections.Generic;
using lab_1.Models.Entities;

namespace lab_1.Data
{
    public class WorkshopDataContext
    {
        public List<Customer> Customers { get; } = new();
        public List<Vehicle> Vehicles { get; } = new();
        public List<Mechanic> Mechanics { get; } = new();
        public List<ServiceCategory> ServiceCategories { get; } = new();
        public List<ServiceItem> ServiceItems { get; } = new();
        public List<ServiceOrder> ServiceOrders { get; } = new();
        public List<OrderLine> OrderLines { get; } = new();
        public List<AppointmentSlot> AppointmentSlots { get; } = new();

        public WorkshopDataContext()
        {
            WorkshopSeedData.Initialize(this);
        }
    }
}
