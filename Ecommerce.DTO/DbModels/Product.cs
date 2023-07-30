namespace Ecommerce.DTO.DbModels
{
    public class Product
    {
        public Product()
        {
            CartProducts = new HashSet<CartProduct>();
            ProductFeatures = new HashSet<ProductFeature>();
            ProductImages = new HashSet<ProductImage>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string PromoCode { get; set; }
        public long Seller_Id { get; set; }
        public long Category_Id { get; set; }
        public bool IsFlatPrice { get; set; }
        public string Image { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? FlatPrice { get; set; }
        public long Uom_Id { get; set; }
        public string Keyword { get; set; }
        public long Currency_Id { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Category Category { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual UOM UOM { get; set; }

        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<CartProduct> CartProducts { get; set; }
    }
}