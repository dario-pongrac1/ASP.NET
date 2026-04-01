using System.Collections.Generic;

namespace lab_1.Models.Entities
{
    public class ServiceItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int EstimatedDurationMinutes { get; set; }
        public bool RequiresParts { get; set; }

        public int ServiceCategoryId { get; set; }
        public ServiceCategory? Category { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new();
    }
}
