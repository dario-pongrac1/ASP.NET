using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab_1.Models.Entities
{
    public class AppointmentSlot : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Mechanic))]
        public int MechanicId { get; set; }
        public virtual Mechanic? Mechanic { get; set; }
        [Display(Name = "Početak")]
        public DateTime StartAt { get; set; }
        [Display(Name = "Kraj")]
        public DateTime EndAt { get; set; }
        public bool IsReserved { get; set; }
        [ForeignKey(nameof(ServiceOrder))]
        public int? ServiceOrderId { get; set; }
        public virtual ServiceOrder? ServiceOrder { get; set; }
        public DateTime? DeletedAt { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartAt < DateTime.Now)
            {
                yield return new ValidationResult(
                    "Početak termina ne može biti u prošlosti.",
                    new[] { nameof(StartAt) });
            }

            if (EndAt <= StartAt)
            {
                yield return new ValidationResult(
                    "Kraj termina mora biti nakon početka termina.",
                    new[] { nameof(EndAt) });
            }
        }
    }
}
