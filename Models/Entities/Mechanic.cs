using System;
using System.Collections.Generic;

namespace lab_1.Models.Entities
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public bool IsCertified { get; set; }
        public int ExperienceYears { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime EmployedSince { get; set; }

        public List<ServiceOrder> ServiceOrders { get; set; } = new();
        public List<AppointmentSlot> AppointmentSlots { get; set; } = new();
    }
}
