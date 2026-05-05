using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab_1.Models.Entities
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ServiceOrder))]
        public int ServiceOrderId { get; set; }
        public virtual ServiceOrder? ServiceOrder { get; set; }

        [ForeignKey(nameof(ServiceItem))]
        public int ServiceItemId { get; set; }
        public virtual ServiceItem? ServiceItem { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PartCost { get; set; }
    }
}
