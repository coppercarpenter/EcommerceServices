using Ecommerce.DTO.DbModels;

namespace Ecommerce.Services.Interfaces
{
    public interface ISellerService
    {
        Seller AddSeller(string username, string password, string firstname, string lastname, string email,
                         string website, string companyAddress, string fax, long cityId);

        Seller EditSeller(long id, string username, string firstname, string lastname, string email,
                          string image, string website, string companyAddress, string fax, long cityId);

        bool RemoveSeller(long id);

        Seller GetSeller(long id);

        IQueryable<Seller> GetSellers();
    }
}