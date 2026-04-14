using lab_1.Models.Entities;

namespace lab_1.Services.Repositories
{
    public interface IWorkshopReadRepository
    {
        IReadOnlyList<Customer> GetCustomers();
        Customer? GetCustomerById(int id);

        IReadOnlyList<Vehicle> GetVehicles();
        Vehicle? GetVehicleById(int id);

        IReadOnlyList<Mechanic> GetMechanics();
        Mechanic? GetMechanicById(int id);

        IReadOnlyList<ServiceOrder> GetServiceOrders();
        ServiceOrder? GetServiceOrderById(int id);

        IReadOnlyList<ServiceCategory> GetServiceCategories();
        ServiceCategory? GetServiceCategoryById(int id);

        IReadOnlyList<ServiceItem> GetServiceItems();
        ServiceItem? GetServiceItemById(int id);

        IReadOnlyList<OrderLine> GetOrderLines();
        OrderLine? GetOrderLineById(int id);

        IReadOnlyList<AppointmentSlot> GetAppointmentSlots();
        AppointmentSlot? GetAppointmentSlotById(int id);
    }
}
