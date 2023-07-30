using Ecommerce.Common.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Common.Helpers
{
    public static class AppSettingHelper
    {
        #region Private Methods

        private static string GetSettingValue(string parentKey, string childKey)
        {
            IConfigurationRoot configuration = GetSettingConfiguration();

            if (!configuration.GetSection(parentKey).Exists())
            {
                throw new NotFoundException(parentKey);
            }

            if (!configuration.GetSection(parentKey).GetSection(childKey).Exists())
            {
                throw new NotFoundException(childKey);
            }

            return configuration.GetSection(parentKey).GetSection(childKey).Value;
        }

        private static IConfigurationRoot GetSettingConfiguration()
        {
            var env = string.Empty;
            var currentEnv = $"{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}";
            if (!string.IsNullOrWhiteSpace(currentEnv))
                env = $".{currentEnv}";

            return new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                         .AddJsonFile($"appsettings{env}.json")
                        .Build();
        }

        #endregion Private Methods

        #region Fields

        #region Parent Sections

        public const string CustomSettings = "Custom_Settings";

        public const string FilePaths = "File_Paths";

        #endregion Parent Sections

        #region Child Sections

        public const string JwtTokenSecret = "Jwt_Token_Secret";

        public const string JwtValueSecret = "Jwt_Value_Secret";

        public const string PasswordSalt = "Password_Salt";

        public const string PasswordSecret = "Password_Secret";

        public const string RabbitMQHost = "Rabbit_MQ_Host";

        public const string RabbitMQPort = "Rabbit_MQ_Port";

        public const string BaseURL = "Base_Url";

        public const string HostUrl = "Host_Url";

        public const string UserBaseURL = "User_Base_URL";

        public const string SellerPath = "Seller_Path";

        public const string ProductPath = "Product_Path";

        public const string CountryPath = "Country_Path";

        public const string UserPath = "User_Path";

        public const string DataPath = "Data_Path";

        #endregion Child Sections

        #endregion Fields

        #region Methods

        #region Custom Settings

        public static string GetJwtTokenSecret()
        {
            return GetSettingValue(CustomSettings, JwtTokenSecret);
        }

        public static string GetJwtValueSecret()
        {
            return GetSettingValue(CustomSettings, JwtValueSecret);
        }

        public static string GetPasswordSalt()
        {
            return GetSettingValue(CustomSettings, PasswordSalt);
        }

        public static string GetPasswordSecret()
        {
            return GetSettingValue(CustomSettings, PasswordSecret);
        }

        public static string GetRabbitMQHost()
        {
            return GetSettingValue(CustomSettings, RabbitMQHost);
        }

        public static int GetRabbitMQPort()
        {
            return int.Parse(GetSettingValue(CustomSettings, RabbitMQPort));
        }

        public static string GetBaseURL()
        {
            return GetSettingValue(CustomSettings, BaseURL);
        }

        public static string GetUserBaseURL()
        {
            return GetSettingValue(CustomSettings, UserBaseURL);
        }

        public static string GetHostURL()
        {
            return GetSettingValue(CustomSettings, HostUrl);
        }

        #endregion Custom Settings

        #region Connection Strings

        public static string GetDefaultConnection()
        {
            return GetSettingValue("ConnectionStrings", "DefaultConnection");
        }

        #endregion Connection Strings

        #region File Paths

        public static string GetProductPath()
        {
            return GetSettingValue(FilePaths, ProductPath);
        }

        public static string GetSellerPath()
        {
            return GetSettingValue(FilePaths, SellerPath);
        }

        public static string GetCountryPath()
        {
            return GetSettingValue(FilePaths, CountryPath);
        }

        public static string GetUserPath()
        {
            return GetSettingValue(FilePaths, UserPath);
        }

        public static string GetDataPath()
        {
            return GetSettingValue(FilePaths, DataPath);
        }

        #endregion File Paths

        #endregion Methods
    }
}