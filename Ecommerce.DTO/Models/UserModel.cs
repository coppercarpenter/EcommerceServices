using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.DTO.Models
{
    #region Request

    public class AddUserRequest
    {
        [JsonPropertyName("username")]
        [Required(ErrorMessage = "username is required")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }

        [JsonPropertyName("phone_number")]
        [Required(ErrorMessage = "Do not enter more than 16 characters"), MaxLength(16)]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "email is required"), EmailAddress(ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
    }

    public class EditUserRequest
    {
        [JsonPropertyName("username")]
        [Required(ErrorMessage = "username is required")]
        public string Username { get; set; }

        [JsonPropertyName("phone_number")]
        [Required(ErrorMessage = "Do not enter more than 16 characters"), MaxLength(16)]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "email is required"), EmailAddress(ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        #endregion Request
    }

    #region Response

    public class UserResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }

    #endregion Response
}