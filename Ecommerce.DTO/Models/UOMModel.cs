using System.Text.Json.Serialization;

namespace Ecommerce.DTO.Models
{
    #region Request

    public class AddUOMRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }

    public class EditUOMRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }

    #endregion Request

    #region Response

    public class UOMResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }

    #endregion Response
}