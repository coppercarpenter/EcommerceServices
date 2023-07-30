using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class UOMRepository : RepositoryBase<UOM>, IUOMRepository
    {
        #region Constructors

        public UOMRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors

        public bool AnyUOM(long id)
        {
            return FindAll().Any(f => f.Id == id);
        }

        public UOM GetUOM(long id)
        {
            return FindAll().FirstOrDefault(f => f.Id == id);
        }

        public IQueryable<UOM> GetUOMs()
        {
            return FindAll();
        }
    }
}