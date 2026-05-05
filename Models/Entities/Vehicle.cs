using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using lab_1.Models.Enums;

namespace lab_1.Models.Entities
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string Vin { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public int MileageKm { get; set; }
        public VehicleFuelType FuelType { get; set; }
        public DateTime LastServiceDate { get; set; }

        [ForeignKey(nameof(Owner))]
        public int CustomerId { get; set; }
        public virtual Customer? Owner { get; set; }
        public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
    }
}
