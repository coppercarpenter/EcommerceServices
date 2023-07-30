using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface IUserService
    {
        User AddUser(string username, string email, string password, string phoneNumber);

        User EditUser(long id, string username, string email, string phoneNumber);

        bool RemoveUser(long id);

        User GetUser(long id);

        IQueryable<User> GetUsers();
    }
}