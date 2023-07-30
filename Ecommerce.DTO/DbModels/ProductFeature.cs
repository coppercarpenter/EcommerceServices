namespace Ecommerce.DTO.DbModels
{
    public class ProductFeature
    {
        public long Id { get; set; }
        public long Product_Id { get; set; }
        public string FeatureKey { get; set; }
        public string FeatureValue { get; set; }

        public virtual Product Product { get; set; }
    }
}