using Ecommerce.Common;
using Ecommerce.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecommerce.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class CheckJwtAttribute : Attribute, IFilterFactory
    {
        #region Properties

        public bool IsReusable => false;
        public AccountType[] Allows { get; set; }

        #endregion Properties

        #region Methods

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            CheckJwtFilter filter = (CheckJwtFilter)serviceProvider.GetService(typeof(CheckJwtFilter));


            if (Allows != null)
            {
                filter.Allows = Allows.ToList();
            }

            return filter;
        }

        #endregion Methods
    }
}