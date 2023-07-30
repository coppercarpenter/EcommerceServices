using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Unit;
using Ecommerce.Repositories.Interfaces.Unit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Repositories
{
    public static class RepositoryRegistration
    {
        #region Methods

        public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEf(configuration);

            services.AddScoped<IRepositoryUnit, RepositoryUnit>();
        }

        #endregion Methods
    }
}