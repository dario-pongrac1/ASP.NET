using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab_1.Models.Entities
{
    public class ServiceCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ServiceItem> Services { get; set; } = new List<ServiceItem>();
    }
}
