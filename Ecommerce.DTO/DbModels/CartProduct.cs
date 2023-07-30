namespace Ecommerce.DTO.DbModels
{
    public class CartProduct
    {
        public long Id { get; set; }
        public long Customer_Id { get; set; }
        public long Product_Id { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedOn { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}