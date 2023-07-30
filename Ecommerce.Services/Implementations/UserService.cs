using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class UserService : IUserService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public UserService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public User AddUser(string username, string email, string password, string phoneNumber)
        {
            if (_repo.User.GetUsers().Any(f => f.Email.Equals(email)))
                throw new AlreadyExistException("Email");

            if (_repo.User.GetUsers().Any(f => f.Username.Equals(username)))
                throw new AlreadyExistException("Username");

            var user = new User()
            {
                Username = username,
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber,
            };

            _repo.User.Create(user);
            _repo.Save();

            return user;
        }

        public bool RemoveUser(long id)
        {
            var user = GetUser(id) ??
                throw new NotFoundException("User");

            _repo.User.Delete(user);
            _repo.Save();

            return true;
        }

        public User GetUser(long id)
        {
            return _repo.User.GetUser(id);
        }

        public User EditUser(long id, string username, string email, string phoneNumber)
        {
            if (_repo.User.GetUsers().Any(f => f.Email.Equals(email) && f.Id != id))
                throw new AlreadyExistException("Email");

            if (_repo.User.GetUsers().Any(f => f.Username.Equals(username) && f.Id != id))
                throw new AlreadyExistException("Username");

            var user = GetUser(id) ??
                throw new NotFoundException("User");

            user.Username = username;
            user.Email = email;
            user.PhoneNumber = phoneNumber;

            _repo.User.Update(user);
            _repo.Save();

            return user;
        }

        public IQueryable<User> GetUsers()
        {
            return _repo.User.GetUsers();
        }

        #endregion Methods
    }
}