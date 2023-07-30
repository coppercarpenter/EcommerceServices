namespace Ecommerce.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        #region Constructor

        public NotFoundException(string message) : base($"{message} not found")
        {
        }

        #endregion Constructor
    }
}