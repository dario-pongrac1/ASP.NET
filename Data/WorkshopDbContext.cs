using lab_1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace lab_1.Data
{
    public class WorkshopDbContext : DbContext
    {
        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<Mechanic> Mechanics => Set<Mechanic>();
        public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
        public DbSet<ServiceItem> ServiceItems => Set<ServiceItem>();
        public DbSet<ServiceOrder> ServiceOrders => Set<ServiceOrder>();
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();
        public DbSet<AppointmentSlot> AppointmentSlots => Set<AppointmentSlot>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Mechanic>()
                .Property(mechanic => mechanic.HourlyRate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ServiceItem>()
                .Property(serviceItem => serviceItem.BasePrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ServiceOrder>()
                .Property(serviceOrder => serviceOrder.DiscountPercent)
                .HasPrecision(5, 2);

            modelBuilder.Entity<OrderLine>()
                .Property(orderLine => orderLine.UnitPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<OrderLine>()
                .Property(orderLine => orderLine.PartCost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Customer>()
                .HasMany(customer => customer.Vehicles)
                .WithOne(vehicle => vehicle.Owner)
                .HasForeignKey(vehicle => vehicle.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasMany(customer => customer.ServiceOrders)
                .WithOne(serviceOrder => serviceOrder.Customer)
                .HasForeignKey(serviceOrder => serviceOrder.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehicle>()
                .HasMany(vehicle => vehicle.ServiceOrders)
                .WithOne(serviceOrder => serviceOrder.Vehicle)
                .HasForeignKey(serviceOrder => serviceOrder.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mechanic>()
                .HasMany(mechanic => mechanic.ServiceOrders)
                .WithOne(serviceOrder => serviceOrder.Mechanic)
                .HasForeignKey(serviceOrder => serviceOrder.MechanicId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mechanic>()
                .HasMany(mechanic => mechanic.AppointmentSlots)
                .WithOne(appointmentSlot => appointmentSlot.Mechanic)
                .HasForeignKey(appointmentSlot => appointmentSlot.MechanicId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceCategory>()
                .HasMany(serviceCategory => serviceCategory.Services)
                .WithOne(serviceItem => serviceItem.Category)
                .HasForeignKey(serviceItem => serviceItem.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceOrder>()
                .HasMany(serviceOrder => serviceOrder.Lines)
                .WithOne(orderLine => orderLine.ServiceOrder)
                .HasForeignKey(orderLine => orderLine.ServiceOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceItem>()
                .HasMany(serviceItem => serviceItem.OrderLines)
                .WithOne(orderLine => orderLine.ServiceItem)
                .HasForeignKey(orderLine => orderLine.ServiceItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppointmentSlot>()
                .HasOne(appointmentSlot => appointmentSlot.ServiceOrder)
                .WithMany()
                .HasForeignKey(appointmentSlot => appointmentSlot.ServiceOrderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}