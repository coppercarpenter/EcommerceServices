using Ecommerce.DTO.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.DTO.Models
{
    #region Request

    public class AddCountryRequest
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "country name is required")]
        public string Name { get; set; }

        [JsonPropertyName("flag")]
        public FileRequestModel Flag { get; set; }
    }

    public class EditCountryRequest
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "country name is required")]
        public string Name { get; set; }

        [JsonPropertyName("flag")]
        public FileRequestModel Flag { get; set; }
    }

    #endregion Request

    #region Response

    public class CountryResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public FileResponse Flag { get; set; }
    }

    #endregion Response
}