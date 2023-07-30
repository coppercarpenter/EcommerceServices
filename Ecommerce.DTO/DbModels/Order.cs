namespace Ecommerce.DTO.DbModels
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long Id { get; set; }
        public long Customer_Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string ShippingTerms { get; set; }
        public string InvoiceNumber { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}