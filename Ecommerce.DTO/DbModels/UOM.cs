namespace Ecommerce.DTO.DbModels
{
    public class UOM
    {
        public UOM()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Products = new HashSet<Product>();
        }

        public long Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}