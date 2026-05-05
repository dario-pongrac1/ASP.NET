using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab_1.Models.Entities
{
    public class Mechanic
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public bool IsCertified { get; set; }
        public int ExperienceYears { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime EmployedSince { get; set; }

        public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
        public virtual ICollection<AppointmentSlot> AppointmentSlots { get; set; } = new List<AppointmentSlot>();
    }
}
