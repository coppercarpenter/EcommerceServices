using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class UOMService : IUOMService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public UOMService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public IQueryable<UOM> GetUOMs()
        {
            return _repo.UOM.GetUOMs();
        }

        public UOM GetUOM(long id)
        {
            return _repo.UOM.GetUOM(id);
        }

        public UOM AddUOM(string title)
        {
            if (GetUOMs().Any(a => a.Title.Equals(title))) throw new AlreadyExistException("UOM");

            var uom = new UOM
            {
                Title = title
            };

            _repo.UOM.Create(uom);
            _repo.Save();

            return uom;
        }

        public bool RemoveUOM(long id)
        {
            var uom = GetUOM(id) ?? throw new NotFoundException("uom");

            _repo.UOM.Delete(uom);
            _repo.Save();

            return true;
        }

        #endregion Methods
    }
}