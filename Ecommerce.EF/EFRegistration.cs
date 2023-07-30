using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.EF
{
    public static class EFRegistration
    {
        #region Methods

        public static void AddEf(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EContext>();
        }

        #endregion Methods
    }
}