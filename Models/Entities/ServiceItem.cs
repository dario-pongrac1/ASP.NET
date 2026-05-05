using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab_1.Models.Entities
{
    public class ServiceItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int EstimatedDurationMinutes { get; set; }
        public bool RequiresParts { get; set; }

        [ForeignKey(nameof(Category))]
        public int ServiceCategoryId { get; set; }
        public virtual ServiceCategory? Category { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
