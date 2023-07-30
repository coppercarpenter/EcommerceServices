using Ecommerce.Repositories;
using Ecommerce.Services.Implementations.Unit;
using Ecommerce.Services.Interfaces.Unit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Services
{
    public static class ServiceRegistration
    {
        #region Methods

        public static void AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepository(configuration);

            services.AddScoped<IServiceUnit, ServiceUnit>();
        }

        #endregion Methods
    }
}