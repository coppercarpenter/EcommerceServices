using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface IUOMService
    {
        UOM AddUOM(string title);

        UOM GetUOM(long id);

        IQueryable<UOM> GetUOMs();

        bool RemoveUOM(long id);
    }
}