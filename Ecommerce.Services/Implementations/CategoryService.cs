using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{

    internal class CategoryService : ICategoryService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public CategoryService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public Category EditCategory(long id, string name, string description, string image)
        {
            var category = GetCategory(id) ?? throw new NotFoundException("Category");

            category.Name = name;
            category.Description = description;
            category.Image = image;

            _repo.Category.Update(category);
            _repo.Save();

            return category;
        }

        public IQueryable<Category> GetCategories()
        {
            return _repo.Category.GetCategories();
        }

        public Category GetCategory(long id)
        {
            return _repo.Category.GetCategory(id);
        }

        public Category AddCategory(long? parentId, string name, string description, string image)
        {
            if (GetCategories().Any(a => a.Name.Equals(name)))
                throw new AlreadyExistException("Category");

            if (_repo.Category.AnyCategory(parentId.Value))
                throw new NotFoundException("Parent Category");

            var category = new Category
            {
                Name = name,
                Image = image,
                Description = description,
                Parent_Id = parentId,
            };

            _repo.Category.Create(category);
            _repo.Save();

            return category;
        }

        public bool RemoveCategory(long id)
        {
            var category = GetCategory(id) ?? throw new NotFoundException("Category");

            _repo.Category.Delete(category);
            _repo.Save<Category>(category);

            return true;
        }

        #endregion Methods
    }
}