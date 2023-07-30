namespace Ecommerce.Common.Exceptions
{
    public class AlreadyExistException : Exception
    {
        #region Constructor

        public AlreadyExistException(string message) : base($"{message} already exist")
        {
        }

        #endregion Constructor
    }
}