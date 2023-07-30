using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface ICategoryService
    {
        Category AddCategory(long? parentId, string name, string description, string image);

        Category EditCategory(long id, string name, string description, string image);

        Category GetCategory(long id);

        IQueryable<Category> GetCategories();

        bool RemoveCategory(long id);
    }
}