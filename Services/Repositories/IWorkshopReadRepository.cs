using lab_1.Models.Entities;

namespace lab_1.Services.Repositories
{
    public interface IWorkshopReadRepository
    {
        // Read operacije
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

        // Write operacije - CRUD
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);

        void AddVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(int id);

        void AddMechanic(Mechanic mechanic);
        void UpdateMechanic(Mechanic mechanic);
        void DeleteMechanic(int id);

        void AddServiceOrder(ServiceOrder serviceOrder);
        void UpdateServiceOrder(ServiceOrder serviceOrder);
        void DeleteServiceOrder(int id);

        void AddServiceCategory(ServiceCategory serviceCategory);
        void UpdateServiceCategory(ServiceCategory serviceCategory);
        void DeleteServiceCategory(int id);

        void AddServiceItem(ServiceItem serviceItem);
        void UpdateServiceItem(ServiceItem serviceItem);
        void DeleteServiceItem(int id);

        void AddOrderLine(OrderLine orderLine);
        void UpdateOrderLine(OrderLine orderLine);
        void DeleteOrderLine(int id);

        void AddAppointmentSlot(AppointmentSlot appointmentSlot);
        void UpdateAppointmentSlot(AppointmentSlot appointmentSlot);
        void DeleteAppointmentSlot(int id);

        // Save
        Task<int> SaveChangesAsync();
    }
}
