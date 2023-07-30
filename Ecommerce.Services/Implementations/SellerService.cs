using Ecommerce.Common.Exceptions;
using Ecommerce.DTO.DbModels;
using Ecommerce.Repositories.Interfaces.Unit;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    internal class SellerService : ISellerService
    {
        #region Private Fields

        private readonly IRepositoryUnit _repo;

        #endregion Private Fields

        #region Constructors

        public SellerService(IRepositoryUnit repo)
        {
            _repo = repo;
        }

        #endregion Constructors

        #region Methods

        public Seller GetSeller(long id)
        {
            return _repo.Seller.GetSeller(id);
        }

        public IQueryable<Seller> GetSellers()
        {
            return _repo.Seller.GetSellers();
        }

        public Seller AddSeller(string username, string password, string firstname, string lastname, string email,
                                string website, string companyAddress, string fax, long cityId)
        {
            if (!_repo.City.AnyCity(cityId)) throw new NotFoundException("City");

            if (GetSellers().Any(f => f.Username.Equals(username)))
                throw new AlreadyExistException("Username");

            if (GetSellers().Any(f => f.Email.Equals(email)))
                throw new AlreadyExistException("Email");

            var seller = new Seller
            {
                Username = username,
                Password = password,
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                Website = website,
                CompanyAddress = companyAddress,
                Fax = fax,
                City_Id = cityId
            };

            _repo.Seller.Create(seller);
            _repo.Save();

            return seller;
        }

        public Seller EditSeller(long id, string username, string firstname, string lastname,
                                 string email, string image, string website, string companyAddress, string fax,
                                 long cityId)
        {
            if (!_repo.City.AnyCity(cityId)) throw new NotFoundException("City");

            var seller = GetSeller(id) ??
                throw new NotFoundException("Web User");

            if (GetSellers().Any(f => f.Username.Equals(username) && f.Id != id))
                throw new AlreadyExistException("Username");

            if (GetSellers().Any(f => f.Email.Equals(email) && f.Id != id))
                throw new AlreadyExistException("Email");

            seller.City_Id = cityId;
            seller.CompanyAddress = companyAddress;
            seller.Email = email;
            seller.Fax = fax;
            seller.Firstname = firstname;
            seller.Lastname = lastname;
            seller.Username = username;
            seller.Website = website;

            _repo.Seller.Update(seller);
            _repo.Save();

            return seller;
        }

        public bool RemoveSeller(long id)
        {
            var seller = GetSeller(id) ??
                throw new NotFoundException("Seller");

            if (_repo.Product.GetProducts().Any(a => a.Seller_Id == id))
                throw new BadRequestException("Products exist against this supplier cannot delete");

            _repo.Seller.Delete(seller);
            _repo.Save();

            return true;
        }

        #endregion Methods
    }
}