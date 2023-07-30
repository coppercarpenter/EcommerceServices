using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CheckExtensionAttribute : ValidationAttribute
    {
        #region Private Fields

        private readonly string _extension;

        #endregion Private Fields

        #region Constructors

        public CheckExtensionAttribute(string extension)
        {
            _extension = extension;
        }

        #endregion Constructors

        #region Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (_extension.Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }

        #endregion Methods
    }
}