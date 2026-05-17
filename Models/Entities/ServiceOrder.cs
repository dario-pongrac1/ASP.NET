using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using lab_1.Models.Enums;

namespace lab_1.Models.Entities
{
    public class ServiceOrder : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Planirani termin")]
        public DateTime ScheduledAt { get; set; }
        public OrderStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
        public decimal DiscountPercent { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        [ForeignKey(nameof(Vehicle))]
        public int VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

        [ForeignKey(nameof(Mechanic))]
        public int MechanicId { get; set; }
        public virtual Mechanic? Mechanic { get; set; }

        public virtual ICollection<OrderLine> Lines { get; set; } = new List<OrderLine>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ScheduledAt < DateTime.Now)
            {
                yield return new ValidationResult(
                    "Planirani termin ne može biti u prošlosti.",
                    new[] { nameof(ScheduledAt) });
            }
        }
    }
}
