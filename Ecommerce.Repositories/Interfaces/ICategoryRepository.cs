using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Category GetCategory(long id);

        bool AnyCategory(long id);

        IQueryable<Category> GetCategories();
    }
}