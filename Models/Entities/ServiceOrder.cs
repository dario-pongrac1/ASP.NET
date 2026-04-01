using System;
using System.Collections.Generic;
using lab_1.Models.Enums;

namespace lab_1.Models.Entities
{
    public class ServiceOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ScheduledAt { get; set; }
        public OrderStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
        public decimal DiscountPercent { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        public int MechanicId { get; set; }
        public Mechanic? Mechanic { get; set; }

        public List<OrderLine> Lines { get; set; } = new();
    }
}
