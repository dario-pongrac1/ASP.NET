using System;
using System.Collections.Generic;

namespace lab_1.Models.Entities
{
    public class ServiceCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public List<ServiceItem> Services { get; set; } = new();
    }
}
