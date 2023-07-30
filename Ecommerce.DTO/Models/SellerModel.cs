using Ecommerce.DTO.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.DTO.Models
{
    public class AddSellerRequest
    {
        [JsonPropertyName("first_name")]
        [Required(ErrorMessage = "first name is required")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        [Required(ErrorMessage = "last name is required")]
        public string LastName { get; set; }

        [JsonPropertyName("mobile_number")]
        [Required(ErrorMessage = "mobile number is required")]
        public string Mobile { get; set; }

        [JsonPropertyName("email")]
        [EmailAddress(ErrorMessage = "invalid email"), Required(ErrorMessage = "email is required")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "password")]
        public string Password { get; set; }

        [JsonPropertyName("user_name")]
        [Required(ErrorMessage = "username is required")]
        public string Username { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }

        [JsonPropertyName("company_address")]
        public string CompanyAddres { get; set; }

        [JsonPropertyName("fax")]
        public string Fax { get; set; }

        [JsonPropertyName("city_id")]
        public long City_Id { get; set; }
    }

    public class EditSellerRequest
    {
        [JsonPropertyName("first_name")]
        [Required(ErrorMessage = "first name is required")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        [Required(ErrorMessage = "last name is required")]
        public string LastName { get; set; }

        [JsonPropertyName("mobile_number")]
        [Required(ErrorMessage = "mobile number is required")]
        public string Mobile { get; set; }

        [JsonPropertyName("email")]
        [EmailAddress(ErrorMessage = "invalid email"), Required(ErrorMessage = "email is required")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "password")]
        public string Password { get; set; }

        [JsonPropertyName("user_name")]
        [Required(ErrorMessage = "username is required")]
        public string Username { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }

        [JsonPropertyName("company_address")]
        public string CompanyAddres { get; set; }

        [JsonPropertyName("fax")]
        public string Fax { get; set; }

        [JsonPropertyName("city_id")]
        public long City_Id { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public FileRequestModel Image { get; set; }

        public string CompanyAddress { get; set; }
    }

    public class SellerResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("company_address")]
        public string CompanyAddress { get; set; }

        [JsonPropertyName("image")]
        public FileResponse Image { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }

        [JsonPropertyName("fax")]
        public string Fax { get; set; }

        [JsonPropertyName("city_id")]
        public long City_Id { get; set; }

        [JsonPropertyName("city_name")]
        public string CityName { get; set; }
    }
}