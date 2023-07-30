namespace Ecommerce.DTO.DbModels
{
    public class Currency
    {
        public Currency()
        {
            Products = new HashSet<Product>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string CurrencySymbol { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}