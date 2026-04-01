using System;
using System.Collections.Generic;
using lab_1.Models.Enums;

namespace lab_1.Models.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Vin { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public int MileageKm { get; set; }
        public VehicleFuelType FuelType { get; set; }
        public DateTime LastServiceDate { get; set; }

        public int CustomerId { get; set; }
        public Customer? Owner { get; set; }
        public List<ServiceOrder> ServiceOrders { get; set; } = new();
    }
}
