using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.DTO.Models
{
    #region Request

    public class AddToCartModel
    {
        [JsonPropertyName("product_id")]
        public long Product_Id { get; set; }

        [JsonPropertyName("quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid quantity.")]
        public int Quantity { get; set; }
    }

    public class AddOrderRequest
    {
        [JsonPropertyName("shipping_address")]
        public long ShippingAddress { get; set; }

        [JsonPropertyName("shipping_terms")]
        public string ShippingTerms { get; set; }

        public List<AddToCartModel> List { get; set; }
    }

    #endregion Request

    #region Response

    public class CartResponse
    {
        [JsonPropertyName("total_quantity")]
        public int TotalQuantity { get; set; }

        [JsonPropertyName("product")]
        public ProductResponse Product { get; set; }
    }

    public class OrderDetailResponse
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unit_of_measure_id")]
        public long UnitOfMasure_Id { get; set; }

        [JsonPropertyName("unit_of_measure")]
        public string UnitOfMasure { get; set; }

        [JsonPropertyName("currency_id")]
        public long Currency_Id { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("price_per_unit")]
        public decimal PerUnitPrice { get; set; }
    }

    public class OrderResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonPropertyName("shipping_terms")]
        public string ShippingTerms { get; set; }

        [JsonPropertyName("details")]
        public List<OrderDetailResponse> Details { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("total_items")]
        public int TotalItems { get; set; }
    }

    #endregion Response
}