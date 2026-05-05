using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab_1.Models.Entities
{
    public class AppointmentSlot
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Mechanic))]
        public int MechanicId { get; set; }
        public virtual Mechanic? Mechanic { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public bool IsReserved { get; set; }
        [ForeignKey(nameof(ServiceOrder))]
        public int? ServiceOrderId { get; set; }
        public virtual ServiceOrder? ServiceOrder { get; set; }
    }
}
