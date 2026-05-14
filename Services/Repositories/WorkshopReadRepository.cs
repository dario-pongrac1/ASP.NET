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

        public void AddCustomer(Customer customer)
        {
            customer.RegisteredAt = DateTime.UtcNow;
            _context.Customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                customer.DeletedAt = DateTime.UtcNow;
                _context.Customers.Update(customer);
            }
        }

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

        public void AddVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
        }

        public void DeleteVehicle(int id)
        {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle != null)
            {
                vehicle.DeletedAt = DateTime.UtcNow;
                _context.Vehicles.Update(vehicle);
            }
        }

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

        public void AddMechanic(Mechanic mechanic)
        {
            _context.Mechanics.Add(mechanic);
        }

        public void UpdateMechanic(Mechanic mechanic)
        {
            _context.Mechanics.Update(mechanic);
        }

        public void DeleteMechanic(int id)
        {
            var mechanic = _context.Mechanics.Find(id);
            if (mechanic != null)
            {
                mechanic.DeletedAt = DateTime.UtcNow;
                _context.Mechanics.Update(mechanic);
            }
        }

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

        public void AddServiceOrder(ServiceOrder serviceOrder)
        {
            serviceOrder.CreatedAt = DateTime.UtcNow;
            _context.ServiceOrders.Add(serviceOrder);
        }

        public void UpdateServiceOrder(ServiceOrder serviceOrder)
        {
            _context.ServiceOrders.Update(serviceOrder);
        }

        public void DeleteServiceOrder(int id)
        {
            var serviceOrder = _context.ServiceOrders.Find(id);
            if (serviceOrder != null)
            {
                serviceOrder.DeletedAt = DateTime.UtcNow;
                _context.ServiceOrders.Update(serviceOrder);
            }
        }

        public IReadOnlyList<ServiceCategory> GetServiceCategories() => _context.ServiceCategories
            .AsNoTracking()
            .Include(serviceCategory => serviceCategory.Services)
            .ToList();

        public ServiceCategory? GetServiceCategoryById(int id) => _context.ServiceCategories
            .AsNoTracking()
            .Include(serviceCategory => serviceCategory.Services)
            .SingleOrDefault(serviceCategory => serviceCategory.Id == id);

        public void AddServiceCategory(ServiceCategory serviceCategory)
        {
            serviceCategory.CreatedAt = DateTime.UtcNow;
            _context.ServiceCategories.Add(serviceCategory);
        }

        public void UpdateServiceCategory(ServiceCategory serviceCategory)
        {
            _context.ServiceCategories.Update(serviceCategory);
        }

        public void DeleteServiceCategory(int id)
        {
            var serviceCategory = _context.ServiceCategories.Find(id);
            if (serviceCategory != null)
            {
                serviceCategory.DeletedAt = DateTime.UtcNow;
                _context.ServiceCategories.Update(serviceCategory);
            }
        }

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

        public void AddServiceItem(ServiceItem serviceItem)
        {
            _context.ServiceItems.Add(serviceItem);
        }

        public void UpdateServiceItem(ServiceItem serviceItem)
        {
            _context.ServiceItems.Update(serviceItem);
        }

        public void DeleteServiceItem(int id)
        {
            var serviceItem = _context.ServiceItems.Find(id);
            if (serviceItem != null)
            {
                serviceItem.DeletedAt = DateTime.UtcNow;
                _context.ServiceItems.Update(serviceItem);
            }
        }

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

        public void AddOrderLine(OrderLine orderLine)
        {
            _context.OrderLines.Add(orderLine);
        }

        public void UpdateOrderLine(OrderLine orderLine)
        {
            _context.OrderLines.Update(orderLine);
        }

        public void DeleteOrderLine(int id)
        {
            var orderLine = _context.OrderLines.Find(id);
            if (orderLine != null)
            {
                orderLine.DeletedAt = DateTime.UtcNow;
                _context.OrderLines.Update(orderLine);
            }
        }

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

        public void AddAppointmentSlot(AppointmentSlot appointmentSlot)
        {
            _context.AppointmentSlots.Add(appointmentSlot);
        }

        public void UpdateAppointmentSlot(AppointmentSlot appointmentSlot)
        {
            _context.AppointmentSlots.Update(appointmentSlot);
        }

        public void DeleteAppointmentSlot(int id)
        {
            var appointmentSlot = _context.AppointmentSlots.Find(id);
            if (appointmentSlot != null)
            {
                appointmentSlot.DeletedAt = DateTime.UtcNow;
                _context.AppointmentSlots.Update(appointmentSlot);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
