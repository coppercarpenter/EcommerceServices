using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.DTO.Models
{
    #region Request

    public class AddCityRequest
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }

        [JsonPropertyName("country_id")]
        public long Country_Id { get; set; }
    }

    public class EditCityRequest
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }

        [JsonPropertyName("country_id")]
        public long Country_Id { get; set; }
    }

    #endregion Request

    #region Response

    public class CityResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("country_id")]
        public long Country_Id { get; set; }
    }

    #endregion Response
}