using System;
using System.Collections.Generic;
using System.Linq;
using lab_1.Models.Entities;
using lab_1.Models.Enums;

namespace lab_1.Data
{
    public static class WorkshopSeedData
    {
        public static void Initialize(WorkshopDbContext context)
        {
            if (context.Customers.Any())
            {
                return;
            }

            // Add categories first
            var categories = new List<ServiceCategory>
            {
                new() { Name = "Regularni servis", Description = "Osnovno održavanje vozila.", CreatedAt = new DateTime(2024, 1, 1) },
                new() { Name = "Dijagnostika", Description = "Dijagnostika i pronalazak grešaka.", CreatedAt = new DateTime(2024, 1, 1) },
                new() { Name = "Kočioni sustav", Description = "Zamjena i servis kočnica.", CreatedAt = new DateTime(2024, 1, 1) }
            };
            context.ServiceCategories.AddRange(categories);
            context.SaveChanges();

            // Add service items
            var services = new List<ServiceItem>
            {
                new() { Name = "Zamjena ulja", Description = "Motorno ulje + filter ulja.", BasePrice = 60m, EstimatedDurationMinutes = 45, RequiresParts = true, ServiceCategoryId = categories[0].Id },
                new() { Name = "Zamjena filtera zraka", Description = "Novi filter zraka motora.", BasePrice = 25m, EstimatedDurationMinutes = 20, RequiresParts = true, ServiceCategoryId = categories[0].Id },
                new() { Name = "OBD dijagnostika", Description = "Očitanje i analiza grešaka.", BasePrice = 35m, EstimatedDurationMinutes = 30, RequiresParts = false, ServiceCategoryId = categories[1].Id },
                new() { Name = "Zamjena pločica", Description = "Prednje kočione pločice.", BasePrice = 120m, EstimatedDurationMinutes = 90, RequiresParts = true, ServiceCategoryId = categories[2].Id },
                new() { Name = "Servis klima uredaja", Description = "Punjenje i dezinfekcija klime.", BasePrice = 80m, EstimatedDurationMinutes = 60, RequiresParts = true, ServiceCategoryId = categories[0].Id }
            };
            context.ServiceItems.AddRange(services);
            context.SaveChanges();

            // Add mechanics
            var mechanics = new List<Mechanic>
            {
                new() { FirstName = "Ivan", LastName = "Horvat", Specialty = "Mehanika motora", IsCertified = true, ExperienceYears = 8, HourlyRate = 32m, EmployedSince = new DateTime(2019, 3, 5) },
                new() { FirstName = "Marko", LastName = "Kovacic", Specialty = "Dijagnostika", IsCertified = true, ExperienceYears = 6, HourlyRate = 34m, EmployedSince = new DateTime(2020, 5, 1) },
                new() { FirstName = "Ana", LastName = "Boric", Specialty = "Kocioni sustavi", IsCertified = true, ExperienceYears = 9, HourlyRate = 36m, EmployedSince = new DateTime(2018, 2, 15) }
            };
            context.Mechanics.AddRange(mechanics);
            context.SaveChanges();

            // Add customers
            var customers = new List<Customer>
            {
                new() { FirstName = "Luka", LastName = "Peric", PhoneNumber = "+38591111222", Email = "luka.peric@example.com", Address = "Savska 10, Zagreb", RegisteredAt = new DateTime(2025, 6, 1) },
                new() { FirstName = "Petra", LastName = "Novak", PhoneNumber = "+38595555333", Email = "petra.novak@example.com", Address = "Vukovarska 21, Zagreb", RegisteredAt = new DateTime(2025, 8, 20) },
                new() { FirstName = "Mia", LastName = "Klaric", PhoneNumber = "+38598888777", Email = "mia.klaric@example.com", Address = "Maksimirska 88, Zagreb", RegisteredAt = new DateTime(2025, 10, 3) }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();

            // Add vehicles
            var vehicles = new List<Vehicle>
            {
                new() { Vin = "WAUZZZ8V0GA123456", LicensePlate = "ZG-1234-AA", Manufacturer = "Audi", Model = "A3", Year = 2017, MileageKm = 132000, FuelType = VehicleFuelType.Diesel, LastServiceDate = new DateTime(2025, 11, 11), CustomerId = customers[0].Id },
                new() { Vin = "WVWZZZAUZKW987654", LicensePlate = "ZG-6789-BB", Manufacturer = "Volkswagen", Model = "Golf", Year = 2019, MileageKm = 98000, FuelType = VehicleFuelType.Petrol, LastServiceDate = new DateTime(2025, 12, 5), CustomerId = customers[1].Id },
                new() { Vin = "TMBJJ7NE0M0456123", LicensePlate = "ZG-5555-CC", Manufacturer = "Skoda", Model = "Octavia", Year = 2021, MileageKm = 72000, FuelType = VehicleFuelType.Hybrid, LastServiceDate = new DateTime(2025, 10, 14), CustomerId = customers[2].Id }
            };
            context.Vehicles.AddRange(vehicles);
            context.SaveChanges();

            // Add service orders
            var orders = new List<ServiceOrder>
            {
                new() { OrderNumber = "SO-2026-001", CreatedAt = new DateTime(2026, 3, 25, 8, 0, 0), ScheduledAt = new DateTime(2026, 4, 2, 9, 0, 0), Status = OrderStatus.Scheduled, Notes = "Klijent cuje cudan zvuk kod kocenja.", DiscountPercent = 0m, CustomerId = customers[0].Id, VehicleId = vehicles[0].Id, MechanicId = mechanics[2].Id },
                new() { OrderNumber = "SO-2026-002", CreatedAt = new DateTime(2026, 3, 27, 10, 30, 0), ScheduledAt = new DateTime(2026, 4, 2, 11, 0, 0), Status = OrderStatus.InProgress, Notes = "Redovni servis + klima.", DiscountPercent = 5m, CustomerId = customers[1].Id, VehicleId = vehicles[1].Id, MechanicId = mechanics[0].Id },
                new() { OrderNumber = "SO-2026-003", CreatedAt = new DateTime(2026, 3, 29, 14, 45, 0), ScheduledAt = new DateTime(2026, 4, 3, 8, 30, 0), Status = OrderStatus.Scheduled, Notes = "Upaljena check engine lampica.", DiscountPercent = 0m, CustomerId = customers[2].Id, VehicleId = vehicles[2].Id, MechanicId = mechanics[1].Id }
            };
            context.ServiceOrders.AddRange(orders);
            context.SaveChanges();

            // Add order lines
            var orderLines = new List<OrderLine>
            {
                new() { ServiceOrderId = orders[0].Id, ServiceItemId = services[3].Id, Quantity = 1, UnitPrice = 120m, PartCost = 45m },
                new() { ServiceOrderId = orders[0].Id, ServiceItemId = services[2].Id, Quantity = 1, UnitPrice = 35m, PartCost = 0m },
                new() { ServiceOrderId = orders[1].Id, ServiceItemId = services[0].Id, Quantity = 1, UnitPrice = 60m, PartCost = 28m },
                new() { ServiceOrderId = orders[1].Id, ServiceItemId = services[1].Id, Quantity = 1, UnitPrice = 25m, PartCost = 12m },
                new() { ServiceOrderId = orders[1].Id, ServiceItemId = services[4].Id, Quantity = 1, UnitPrice = 80m, PartCost = 30m },
                new() { ServiceOrderId = orders[2].Id, ServiceItemId = services[2].Id, Quantity = 1, UnitPrice = 35m, PartCost = 0m },
                new() { ServiceOrderId = orders[2].Id, ServiceItemId = services[0].Id, Quantity = 1, UnitPrice = 60m, PartCost = 28m }
            };
            context.OrderLines.AddRange(orderLines);
            context.SaveChanges();

            // Add appointment slots
            var slots = new List<AppointmentSlot>
            {
                new() { MechanicId = mechanics[2].Id, StartAt = new DateTime(2026, 4, 2, 9, 0, 0), EndAt = new DateTime(2026, 4, 2, 10, 30, 0), IsReserved = true, ServiceOrderId = orders[0].Id },
                new() { MechanicId = mechanics[0].Id, StartAt = new DateTime(2026, 4, 2, 11, 0, 0), EndAt = new DateTime(2026, 4, 2, 13, 0, 0), IsReserved = true, ServiceOrderId = orders[1].Id },
                new() { MechanicId = mechanics[1].Id, StartAt = new DateTime(2026, 4, 3, 8, 30, 0), EndAt = new DateTime(2026, 4, 3, 10, 0, 0), IsReserved = true, ServiceOrderId = orders[2].Id },
                new() { MechanicId = mechanics[1].Id, StartAt = new DateTime(2026, 4, 3, 10, 30, 0), EndAt = new DateTime(2026, 4, 3, 12, 0, 0), IsReserved = false, ServiceOrderId = null }
            };
            context.AppointmentSlots.AddRange(slots);
            context.SaveChanges();
        }
    }
}
