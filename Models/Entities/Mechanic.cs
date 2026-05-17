using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab_1.Models.Entities
{
    public class Mechanic : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public bool IsCertified { get; set; }
        public int ExperienceYears { get; set; }
        public decimal HourlyRate { get; set; }
        [Display(Name = "Zaposlen od")]
        public DateTime EmployedSince { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
        public virtual ICollection<AppointmentSlot> AppointmentSlots { get; set; } = new List<AppointmentSlot>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EmployedSince.Date > DateTime.Today)
            {
                yield return new ValidationResult(
                    "Datum zaposlenja ne može biti u budućnosti.",
                    new[] { nameof(EmployedSince) });
            }
        }
    }
}
