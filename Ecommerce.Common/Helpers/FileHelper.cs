namespace Ecommerce.Common.Helpers
{
    public static class FileHelper
    {
        #region Private Methods

        private static bool AddFile(string fileContent, string path)
        {
            byte[] byt = Convert.FromBase64String(fileContent);

            File.WriteAllBytes(path, byt);

            return true;
        }

        #endregion Private Methods

        #region Methods

        public static string GetFileLink(string fileName, FileLinkType type)
        {
            var baseURL = AppSettingHelper.GetBaseURL();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return string.Empty;
            }
            return type switch
            {
                FileLinkType.Product => $"{baseURL}/v1/product/{fileName}/view",
                FileLinkType.Country => $"{baseURL}/v1/country/{fileName}/view",
                FileLinkType.Seller => $"{baseURL}/v1/seller/{fileName}/view",
                _ => "/v1/product/image/No_Image.jpeg/view",
            };
        }

        public static long GetFileSize(string file)
        {
            var fileInfo = new FileInfo(file);
            return fileInfo.Exists ? fileInfo.Length : 0;
        }

        public static string UploadFiles(string content, string extension, string folderName)
        {
            var fileName = GetFileName(extension);

            var iconPath = $"{AppSettingHelper.GetDataPath()}{folderName}\\";

            AddFile(content, iconPath + fileName);

            return fileName;
        }

        public static string UploadFiles(string content, string extension, FileLinkType type)
        {
            var fileName = GetFileName(extension);
            var iconPath = string.Empty;

            switch (type)
            {
                case FileLinkType.Product:
                    iconPath = AppSettingHelper.GetProductPath();
                    break;

                case FileLinkType.Seller:
                    iconPath = AppSettingHelper.GetSellerPath();
                    break;

                case FileLinkType.Country:
                    iconPath = AppSettingHelper.GetCountryPath();
                    break;

                default:
                    break;
            }

            AddFile(content, iconPath + fileName);

            return fileName;
        }

        public static string GetFileName(string extension)
        {
            return Guid.NewGuid().ToString() + "." + extension;
        }

        #endregion Methods
    }
}