using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Base;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUser(long id);

        IQueryable<User> GetUsers();
    }
}