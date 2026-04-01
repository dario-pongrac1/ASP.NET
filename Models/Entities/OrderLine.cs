namespace lab_1.Models.Entities
{
    public class OrderLine
    {
        public int Id { get; set; }

        public int ServiceOrderId { get; set; }
        public ServiceOrder? ServiceOrder { get; set; }

        public int ServiceItemId { get; set; }
        public ServiceItem? ServiceItem { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PartCost { get; set; }
    }
}
