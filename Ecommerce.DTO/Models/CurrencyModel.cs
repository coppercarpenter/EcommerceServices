using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.DTO.Models
{
    #region Request

    public class AddCurrencyRequest
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }

    public class EditCurrencyRequest
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }

    #endregion Request

    #region Response

    public class CurrencyResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

    }

    #endregion Response
}