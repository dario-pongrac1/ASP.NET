using lab_1.Data;
using lab_1.Models.Entities;

namespace lab_1.Services.Repositories
{
    public class WorkshopReadRepository : IWorkshopReadRepository
    {
        private readonly WorkshopDataContext _context;

        public WorkshopReadRepository(WorkshopDataContext context)
        {
            _context = context;
        }

        public IReadOnlyList<Customer> GetCustomers() => _context.Customers;
        public Customer? GetCustomerById(int id) => _context.Customers.SingleOrDefault(x => x.Id == id);

        public IReadOnlyList<Vehicle> GetVehicles() => _context.Vehicles;
        public Vehicle? GetVehicleById(int id) => _context.Vehicles.SingleOrDefault(x => x.Id == id);

        public IReadOnlyList<Mechanic> GetMechanics() => _context.Mechanics;
        public Mechanic? GetMechanicById(int id) => _context.Mechanics.SingleOrDefault(x => x.Id == id);

        public IReadOnlyList<ServiceOrder> GetServiceOrders() => _context.ServiceOrders;
        public ServiceOrder? GetServiceOrderById(int id) => _context.ServiceOrders.SingleOrDefault(x => x.Id == id);

        public IReadOnlyList<ServiceCategory> GetServiceCategories() => _context.ServiceCategories;
        public ServiceCategory? GetServiceCategoryById(int id) => _context.ServiceCategories.SingleOrDefault(x => x.Id == id);

        public IReadOnlyList<ServiceItem> GetServiceItems() => _context.ServiceItems;
        public ServiceItem? GetServiceItemById(int id) => _context.ServiceItems.SingleOrDefault(x => x.Id == id);

        public IReadOnlyList<OrderLine> GetOrderLines() => _context.OrderLines;
        public OrderLine? GetOrderLineById(int id) => _context.OrderLines.SingleOrDefault(x => x.Id == id);

        public IReadOnlyList<AppointmentSlot> GetAppointmentSlots() => _context.AppointmentSlots;
        public AppointmentSlot? GetAppointmentSlotById(int id) => _context.AppointmentSlots.SingleOrDefault(x => x.Id == id);
    }
}
