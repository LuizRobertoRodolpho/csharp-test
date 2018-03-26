namespace PortalTelemedicina.Repository.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal CurrentPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
