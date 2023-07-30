using Ecommerce.DTO.DbModels;
using Ecommerce.EF;
using Ecommerce.Repositories.Implementations.Base;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories.Implementations
{
    internal class UserRepository : RepositoryBase<User>, IUserRepository
    {
        #region Constructors

        public UserRepository(EContext db)
            : base(db)
        {
        }

        #endregion Constructors



        #region Methods

        public IQueryable<User> GetUsers()
        {
            return FindAll();
        }

        public User GetUser(long id)
        {
            return FindAll().FirstOrDefault(a => a.Id == id);
        }

        #endregion Methods
    }
}