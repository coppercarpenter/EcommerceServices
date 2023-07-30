using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        #region Constructors

        public CategoryRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors

        public bool AnyCategory(long id)
        {
            return FindAll().Any(a => a.Id == id);
        }

        public IQueryable<Category> GetCategories()
        {
            return FindAll();
        }

        public Category GetCategory(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }
    }
}