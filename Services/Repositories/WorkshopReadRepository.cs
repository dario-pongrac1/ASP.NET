using lab_1.Data;
using lab_1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace lab_1.Services.Repositories
{
    public class WorkshopReadRepository : IWorkshopReadRepository
    {
        private readonly WorkshopDbContext _context;

        public WorkshopReadRepository(WorkshopDbContext context)
        {
            _context = context;
        }

        public IReadOnlyList<Customer> GetCustomers() => _context.Customers
            .AsNoTracking()
            .Include(customer => customer.Vehicles)
            .Include(customer => customer.ServiceOrders)
            .ToList();

        public Customer? GetCustomerById(int id) => _context.Customers
            .AsNoTracking()
            .Include(customer => customer.Vehicles)
            .Include(customer => customer.ServiceOrders)
            .SingleOrDefault(customer => customer.Id == id);

        public IReadOnlyList<Vehicle> GetVehicles() => _context.Vehicles
            .AsNoTracking()
            .Include(vehicle => vehicle.Owner)
            .Include(vehicle => vehicle.ServiceOrders)
            .ToList();

        public Vehicle? GetVehicleById(int id) => _context.Vehicles
            .AsNoTracking()
            .Include(vehicle => vehicle.Owner)
            .Include(vehicle => vehicle.ServiceOrders)
            .SingleOrDefault(vehicle => vehicle.Id == id);

        public IReadOnlyList<Mechanic> GetMechanics() => _context.Mechanics
            .AsNoTracking()
            .Include(mechanic => mechanic.ServiceOrders)
            .Include(mechanic => mechanic.AppointmentSlots)
            .ToList();

        public Mechanic? GetMechanicById(int id) => _context.Mechanics
            .AsNoTracking()
            .Include(mechanic => mechanic.ServiceOrders)
            .Include(mechanic => mechanic.AppointmentSlots)
            .SingleOrDefault(mechanic => mechanic.Id == id);

        public IReadOnlyList<ServiceOrder> GetServiceOrders() => _context.ServiceOrders
            .AsNoTracking()
            .Include(serviceOrder => serviceOrder.Customer)
            .Include(serviceOrder => serviceOrder.Vehicle)
            .Include(serviceOrder => serviceOrder.Mechanic)
            .Include(serviceOrder => serviceOrder.Lines)
                .ThenInclude(line => line.ServiceItem)
            .ToList();

        public ServiceOrder? GetServiceOrderById(int id) => _context.ServiceOrders
            .AsNoTracking()
            .Include(serviceOrder => serviceOrder.Customer)
            .Include(serviceOrder => serviceOrder.Vehicle)
            .Include(serviceOrder => serviceOrder.Mechanic)
            .Include(serviceOrder => serviceOrder.Lines)
                .ThenInclude(line => line.ServiceItem)
            .SingleOrDefault(serviceOrder => serviceOrder.Id == id);

        public IReadOnlyList<ServiceCategory> GetServiceCategories() => _context.ServiceCategories
            .AsNoTracking()
            .Include(serviceCategory => serviceCategory.Services)
            .ToList();

        public ServiceCategory? GetServiceCategoryById(int id) => _context.ServiceCategories
            .AsNoTracking()
            .Include(serviceCategory => serviceCategory.Services)
            .SingleOrDefault(serviceCategory => serviceCategory.Id == id);

        public IReadOnlyList<ServiceItem> GetServiceItems() => _context.ServiceItems
            .AsNoTracking()
            .Include(serviceItem => serviceItem.Category)
            .Include(serviceItem => serviceItem.OrderLines)
            .ToList();

        public ServiceItem? GetServiceItemById(int id) => _context.ServiceItems
            .AsNoTracking()
            .Include(serviceItem => serviceItem.Category)
            .Include(serviceItem => serviceItem.OrderLines)
            .SingleOrDefault(serviceItem => serviceItem.Id == id);

        public IReadOnlyList<OrderLine> GetOrderLines() => _context.OrderLines
            .AsNoTracking()
            .Include(orderLine => orderLine.ServiceOrder)
            .Include(orderLine => orderLine.ServiceItem)
            .ToList();

        public OrderLine? GetOrderLineById(int id) => _context.OrderLines
            .AsNoTracking()
            .Include(orderLine => orderLine.ServiceOrder)
            .Include(orderLine => orderLine.ServiceItem)
            .SingleOrDefault(orderLine => orderLine.Id == id);

        public IReadOnlyList<AppointmentSlot> GetAppointmentSlots() => _context.AppointmentSlots
            .AsNoTracking()
            .Include(appointmentSlot => appointmentSlot.Mechanic)
            .Include(appointmentSlot => appointmentSlot.ServiceOrder)
            .ToList();

        public AppointmentSlot? GetAppointmentSlotById(int id) => _context.AppointmentSlots
            .AsNoTracking()
            .Include(appointmentSlot => appointmentSlot.Mechanic)
            .Include(appointmentSlot => appointmentSlot.ServiceOrder)
            .SingleOrDefault(appointmentSlot => appointmentSlot.Id == id);
    }
}
