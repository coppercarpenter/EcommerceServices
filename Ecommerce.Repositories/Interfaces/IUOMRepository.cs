using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IUOMRepository : IRepositoryBase<UOM>
    {
        UOM GetUOM(long id);

        bool AnyUOM(long id);

        IQueryable<UOM> GetUOMs();
    }
}