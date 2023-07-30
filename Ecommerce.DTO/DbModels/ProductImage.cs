namespace Ecommerce.DTO.DbModels
{
    public class ProductImage
    {
        public long Id { get; set; }
        public long Product_Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }

        public virtual Product Product { get; set; }
    }
}