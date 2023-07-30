using Ecommerce.DTO.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.DTO.Models
{
    #region Request

    public class AddProductRequest
    {
        [JsonPropertyName("product_name")]
        [Required(ErrorMessage = "product title is required")]
        public string Title { get; set; }

        [JsonPropertyName("model")]
        [Required(ErrorMessage = "product model is required")]
        public string Model { get; set; }

        [JsonPropertyName("description")]
        [Required(ErrorMessage = "description is required")]
        public string Description { get; set; }

        [JsonPropertyName("promo_code")]
        public string PromoCode { get; set; }

        [JsonPropertyName("Category_Id")]
        public long Category_Id { get; set; }

        [JsonPropertyName("seller_id")]
        public long? Seller_Id { get; set; }

        [JsonPropertyName("is_flat_price")]
        public bool IsFlatPrice { get; set; }

        [JsonPropertyName("min_price")]
        public decimal? MinPrice { get; set; }

        [JsonPropertyName("max_price")]
        public decimal? MaxPrice { get; set; }

        [JsonPropertyName("flat_price")]
        public decimal? FlatPrice { get; set; }

        [JsonPropertyName("keyword")]
        public string Keyword { get; set; }

        [JsonPropertyName("product_images")]
        public List<FileRequestModel> ProductImages { get; set; }

        [JsonPropertyName("product_features")]
        public List<AddProductFeatureRequest> ProductFeatures { get; set; }

        [JsonPropertyName("image")]
        [Required(ErrorMessage = "Image is required")]
        public FileRequestModel Image { get; set; }

        [JsonPropertyName("uom_id")]
        public long Uow_Id { get; set; }

        [JsonPropertyName("currency")]
        public long Currency_Id { get; set; }
    }

    public class AddProductFeatureRequest
    {
        [JsonPropertyName("feature_key")]
        public string FeatureKey { get; set; }

        [JsonPropertyName("feature_value")]
        public string FeatureValue { get; set; }
    }

    public class EditProductRequest
    {
        [JsonPropertyName("product_name")]
        [Required(ErrorMessage = "product title is required")]
        public string Title { get; set; }

        [JsonPropertyName("model")]
        [Required(ErrorMessage = "product model is required")]
        public string Model { get; set; }

        [JsonPropertyName("description")]
        [Required(ErrorMessage = "description is required")]
        public string Description { get; set; }

        [JsonPropertyName("promo_code")]
        public string PromoCode { get; set; }

        [JsonPropertyName("category_Id")]
        public long Category_Id { get; set; }

        [JsonPropertyName("is_flat_price")]
        public bool IsFlatPrice { get; set; }

        [JsonPropertyName("min_price")]
        public decimal? MinPrice { get; set; }

        [JsonPropertyName("max_price")]
        public decimal? MaxPrice { get; set; }

        [JsonPropertyName("flat_price")]
        public decimal? FlatPrice { get; set; }

        [JsonPropertyName("uom_id")]
        public long Uom_Id { get; set; }

        [JsonPropertyName("keyword")]
        [Required(ErrorMessage = "please add product keyword")]
        public string Keyword { get; set; }

        [JsonPropertyName("product_images")]
        public List<FileRequestModel> ProductImages { get; set; }

        [JsonPropertyName("product_features")]
        public List<AddProductFeatureRequest> ProductFeatures { get; set; }

        [JsonPropertyName("image")]
        [Required(ErrorMessage = "Image is required")]
        public FileRequestModel Image { get; set; }

        [JsonPropertyName("currency")]
        public long Currency_Id { get; set; }
    }

    #endregion Request

    #region Response

    public class ProductResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("product_name")]
        public string Title { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("promo_code")]
        public string PromoCode { get; set; }

        [JsonPropertyName("seller")]
        public SellerResponse Seller { get; set; }

        [JsonPropertyName("category_id")]
        public long Category_Id { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("is_flat_price")]
        public bool IsFlatPrice { get; set; }

        [JsonPropertyName("min_price")]
        public decimal? MinPrice { get; set; }

        [JsonPropertyName("max_price")]
        public decimal? MaxPrice { get; set; }

        [JsonPropertyName("flat_price")]
        public decimal? FlatPrice { get; set; }

        [JsonPropertyName("uom_id")]
        public long Uom_Id { get; set; }

        [JsonPropertyName("keyword")]
        public string Keyword { get; set; }

        [JsonPropertyName("image")]
        public FileResponse Image { get; set; }

        [JsonPropertyName("product_images")]
        public List<FileResponse> ProductImages { get; set; }

        [JsonPropertyName("product_features")]
        public List<ProductFeatureResponse> ProductFeatures { get; set; }

        [JsonPropertyName("uom_name")]
        public string UomName { get; set; }

        [JsonPropertyName("currency")]
        public long Currency_Id { get; set; }

        [JsonPropertyName("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class ProductFeatureResponse
    {
        [JsonPropertyName("feature_key")]
        public string FeatureKey { get; set; }

        [JsonPropertyName("feature_value")]
        public string FeatureValue { get; set; }
    }

    #endregion Response
}