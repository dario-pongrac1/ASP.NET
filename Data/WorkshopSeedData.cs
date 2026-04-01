using System;
using System.Collections.Generic;
using System.Linq;
using lab_1.Models.Entities;
using lab_1.Models.Enums;

namespace lab_1.Data
{
    public static class WorkshopSeedData
    {
        public static void Initialize(WorkshopDataContext context)
        {
            if (context.Customers.Any())
            {
                return;
            }

            var categories = new List<ServiceCategory>
            {
                new() { Id = 1, Name = "Regularni servis", Description = "Osnovno odrzavanje vozila.", CreatedAt = new DateTime(2024, 1, 1) },
                new() { Id = 2, Name = "Dijagnostika", Description = "Dijagnostika i pronalazak gresaka.", CreatedAt = new DateTime(2024, 1, 1) },
                new() { Id = 3, Name = "Kocioni sustav", Description = "Zamjena i servis kocnica.", CreatedAt = new DateTime(2024, 1, 1) }
            };

            var services = new List<ServiceItem>
            {
                new() { Id = 1, Name = "Zamjena ulja", Description = "Motorno ulje + filter ulja.", BasePrice = 60m, EstimatedDurationMinutes = 45, RequiresParts = true, ServiceCategoryId = 1 },
                new() { Id = 2, Name = "Zamjena filtera zraka", Description = "Novi filter zraka motora.", BasePrice = 25m, EstimatedDurationMinutes = 20, RequiresParts = true, ServiceCategoryId = 1 },
                new() { Id = 3, Name = "OBD dijagnostika", Description = "Ocitanje i analiza gresaka.", BasePrice = 35m, EstimatedDurationMinutes = 30, RequiresParts = false, ServiceCategoryId = 2 },
                new() { Id = 4, Name = "Zamjena plocica", Description = "Prednje kocione plocice.", BasePrice = 120m, EstimatedDurationMinutes = 90, RequiresParts = true, ServiceCategoryId = 3 },
                new() { Id = 5, Name = "Servis klima uredaja", Description = "Punjenje i dezinfekcija klime.", BasePrice = 80m, EstimatedDurationMinutes = 60, RequiresParts = true, ServiceCategoryId = 1 }
            };

            foreach (var category in categories)
            {
                category.Services = services.Where(s => s.ServiceCategoryId == category.Id).ToList();
                foreach (var serviceItem in category.Services)
                {
                    serviceItem.Category = category;
                }
            }

            var mechanics = new List<Mechanic>
            {
                new() { Id = 1, FirstName = "Ivan", LastName = "Horvat", Specialty = "Mehanika motora", IsCertified = true, ExperienceYears = 8, HourlyRate = 32m, EmployedSince = new DateTime(2019, 3, 5) },
                new() { Id = 2, FirstName = "Marko", LastName = "Kovacic", Specialty = "Dijagnostika", IsCertified = true, ExperienceYears = 6, HourlyRate = 34m, EmployedSince = new DateTime(2020, 5, 1) },
                new() { Id = 3, FirstName = "Ana", LastName = "Boric", Specialty = "Kocioni sustavi", IsCertified = true, ExperienceYears = 9, HourlyRate = 36m, EmployedSince = new DateTime(2018, 2, 15) }
            };

            var customers = new List<Customer>
            {
                new() { Id = 1, FirstName = "Luka", LastName = "Peric", PhoneNumber = "+38591111222", Email = "luka.peric@example.com", Address = "Savska 10, Zagreb", RegisteredAt = new DateTime(2025, 6, 1) },
                new() { Id = 2, FirstName = "Petra", LastName = "Novak", PhoneNumber = "+38595555333", Email = "petra.novak@example.com", Address = "Vukovarska 21, Zagreb", RegisteredAt = new DateTime(2025, 8, 20) },
                new() { Id = 3, FirstName = "Mia", LastName = "Klaric", PhoneNumber = "+38598888777", Email = "mia.klaric@example.com", Address = "Maksimirska 88, Zagreb", RegisteredAt = new DateTime(2025, 10, 3) }
            };

            var vehicles = new List<Vehicle>
            {
                new() { Id = 1, Vin = "WAUZZZ8V0GA123456", LicensePlate = "ZG-1234-AA", Manufacturer = "Audi", Model = "A3", Year = 2017, MileageKm = 132000, FuelType = VehicleFuelType.Diesel, LastServiceDate = new DateTime(2025, 11, 11), CustomerId = 1 },
                new() { Id = 2, Vin = "WVWZZZAUZKW987654", LicensePlate = "ZG-6789-BB", Manufacturer = "Volkswagen", Model = "Golf", Year = 2019, MileageKm = 98000, FuelType = VehicleFuelType.Petrol, LastServiceDate = new DateTime(2025, 12, 5), CustomerId = 2 },
                new() { Id = 3, Vin = "TMBJJ7NE0M0456123", LicensePlate = "ZG-5555-CC", Manufacturer = "Skoda", Model = "Octavia", Year = 2021, MileageKm = 72000, FuelType = VehicleFuelType.Hybrid, LastServiceDate = new DateTime(2025, 10, 14), CustomerId = 3 }
            };

            foreach (var vehicle in vehicles)
            {
                var owner = customers.Single(c => c.Id == vehicle.CustomerId);
                vehicle.Owner = owner;
                owner.Vehicles.Add(vehicle);
            }

            var orders = new List<ServiceOrder>
            {
                new() { Id = 1, OrderNumber = "SO-2026-001", CreatedAt = new DateTime(2026, 3, 25, 8, 0, 0), ScheduledAt = new DateTime(2026, 4, 2, 9, 0, 0), Status = OrderStatus.Scheduled, Notes = "Klijent cuje cudan zvuk kod kocenja.", DiscountPercent = 0m, CustomerId = 1, VehicleId = 1, MechanicId = 3 },
                new() { Id = 2, OrderNumber = "SO-2026-002", CreatedAt = new DateTime(2026, 3, 27, 10, 30, 0), ScheduledAt = new DateTime(2026, 4, 2, 11, 0, 0), Status = OrderStatus.InProgress, Notes = "Redovni servis + klima.", DiscountPercent = 5m, CustomerId = 2, VehicleId = 2, MechanicId = 1 },
                new() { Id = 3, OrderNumber = "SO-2026-003", CreatedAt = new DateTime(2026, 3, 29, 14, 45, 0), ScheduledAt = new DateTime(2026, 4, 3, 8, 30, 0), Status = OrderStatus.Scheduled, Notes = "Upaljena check engine lampica.", DiscountPercent = 0m, CustomerId = 3, VehicleId = 3, MechanicId = 2 }
            };

            foreach (var order in orders)
            {
                order.Customer = customers.Single(c => c.Id == order.CustomerId);
                order.Vehicle = vehicles.Single(v => v.Id == order.VehicleId);
                order.Mechanic = mechanics.Single(m => m.Id == order.MechanicId);

                order.Customer.ServiceOrders.Add(order);
                order.Vehicle.ServiceOrders.Add(order);
                order.Mechanic.ServiceOrders.Add(order);
            }

            var orderLines = new List<OrderLine>
            {
                new() { Id = 1, ServiceOrderId = 1, ServiceItemId = 4, Quantity = 1, UnitPrice = 120m, PartCost = 45m },
                new() { Id = 2, ServiceOrderId = 1, ServiceItemId = 3, Quantity = 1, UnitPrice = 35m, PartCost = 0m },
                new() { Id = 3, ServiceOrderId = 2, ServiceItemId = 1, Quantity = 1, UnitPrice = 60m, PartCost = 28m },
                new() { Id = 4, ServiceOrderId = 2, ServiceItemId = 2, Quantity = 1, UnitPrice = 25m, PartCost = 12m },
                new() { Id = 5, ServiceOrderId = 2, ServiceItemId = 5, Quantity = 1, UnitPrice = 80m, PartCost = 30m },
                new() { Id = 6, ServiceOrderId = 3, ServiceItemId = 3, Quantity = 1, UnitPrice = 35m, PartCost = 0m },
                new() { Id = 7, ServiceOrderId = 3, ServiceItemId = 1, Quantity = 1, UnitPrice = 60m, PartCost = 28m }
            };

            foreach (var line in orderLines)
            {
                line.ServiceOrder = orders.Single(o => o.Id == line.ServiceOrderId);
                line.ServiceItem = services.Single(s => s.Id == line.ServiceItemId);

                line.ServiceOrder.Lines.Add(line);
                line.ServiceItem.OrderLines.Add(line);
            }

            var slots = new List<AppointmentSlot>
            {
                new() { Id = 1, MechanicId = 3, StartAt = new DateTime(2026, 4, 2, 9, 0, 0), EndAt = new DateTime(2026, 4, 2, 10, 30, 0), IsReserved = true, ServiceOrderId = 1 },
                new() { Id = 2, MechanicId = 1, StartAt = new DateTime(2026, 4, 2, 11, 0, 0), EndAt = new DateTime(2026, 4, 2, 13, 0, 0), IsReserved = true, ServiceOrderId = 2 },
                new() { Id = 3, MechanicId = 2, StartAt = new DateTime(2026, 4, 3, 8, 30, 0), EndAt = new DateTime(2026, 4, 3, 10, 0, 0), IsReserved = true, ServiceOrderId = 3 },
                new() { Id = 4, MechanicId = 2, StartAt = new DateTime(2026, 4, 3, 10, 30, 0), EndAt = new DateTime(2026, 4, 3, 12, 0, 0), IsReserved = false, ServiceOrderId = null }
            };

            foreach (var slot in slots)
            {
                slot.Mechanic = mechanics.Single(m => m.Id == slot.MechanicId);
                slot.Mechanic.AppointmentSlots.Add(slot);
            }

            context.Customers.AddRange(customers);
            context.Vehicles.AddRange(vehicles);
            context.Mechanics.AddRange(mechanics);
            context.ServiceCategories.AddRange(categories);
            context.ServiceItems.AddRange(services);
            context.ServiceOrders.AddRange(orders);
            context.OrderLines.AddRange(orderLines);
            context.AppointmentSlots.AddRange(slots);
        }
    }
}
