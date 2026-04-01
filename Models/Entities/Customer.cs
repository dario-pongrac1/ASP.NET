using System;
using System.Collections.Generic;

namespace lab_1.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; }

        public List<Vehicle> Vehicles { get; set; } = new();
        public List<ServiceOrder> ServiceOrders { get; set; } = new();
    }
}
