namespace Ecommerce.Common.Helpers
{
    public static class EnumHelper
    {
        #region Methods

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static T GetEnumByString<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static bool IsDefined<T>(string value)
        {
            return Enum.IsDefined(typeof(T), value);
        }

        #endregion Methods
    }
}