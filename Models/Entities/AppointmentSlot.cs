using System;

namespace lab_1.Models.Entities
{
    public class AppointmentSlot
    {
        public int Id { get; set; }
        public int MechanicId { get; set; }
        public Mechanic? Mechanic { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public bool IsReserved { get; set; }
        public int? ServiceOrderId { get; set; }
    }
}
