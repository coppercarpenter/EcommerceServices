using Ecommerce.Common;
using Ecommerce.Common.Helpers;
using Ecommerce.Common.JWT;
using Ecommerce.DTO.DbModels;
using Ecommerce.DTO.Models;
using Ecommerce.DTO.Models.Common;
using Ecommerce.Services.Interfaces.Unit;

namespace Ecommerce.Converters
{
    public class Converter
    {
        private readonly IServiceUnit _service;

        public Converter(IServiceUnit service)
        {
            _service = service;
        }

        public UserResponse GetUserResponse(User user)
        {
            var res = new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
            return res;
        }

        public CountryResponse GetCountryResponse(Country country)
        {
            var res = new CountryResponse
            {
                Name = country.Name,
                Id = country.Id,
                Flag = new FileResponse()
                {
                    FileIdentifier = country.Flag
                }
            };

            if (!string.IsNullOrWhiteSpace(country.Flag))
                res.Flag.FilePath = FileHelper.GetFileLink(country.Flag, FileLinkType.Country);

            return res;
        }

        public SellerResponse GetSellerResponse(Seller seller)
        {
            var res = new SellerResponse()
            {
                CompanyAddress = seller.CompanyAddress,
                Id = seller.Id,
                Username = seller.Username,
                Email = seller.Email,
                FirstName = seller.Firstname,
                LastName = seller.Lastname,
                Website = seller.Website,
                Fax = seller.Fax,
                City_Id = seller.City_Id,
                CityName = _service.City.GetCity(seller.City_Id)?.Name,
                Image = new FileResponse()
                {
                    FileIdentifier = seller.Image
                }
            };
            if (!string.IsNullOrWhiteSpace(seller.Image))
                res.Image.FilePath = FileHelper.GetFileLink(seller.Image, FileLinkType.Seller);
            
            return res;
        }

        public TokenModel GetAdminToken(HttpContext context)
        {
            var request = context.Request;
            var authToken = request.Headers["Authorization"].ToString();
            var tokenValue = authToken.Split(" ");
            var token = TokenManger.ValidateToken(tokenValue[1]);

            return token;
        }

        public CityResponse GetCityResponse(City city)
        {
            var cityResponse = new CityResponse()
            {
                Name = city.Name,
                Id = city.Id,
                Country_Id = city.Country_Id
            };

            return cityResponse;
        }

        public ProductResponse GetProductResponse(Product product)
        {
            var res = new ProductResponse
            {
                Title = product.Title,
                Description = product.Description,
                Keyword = product.Keyword,
                PromoCode = product.PromoCode,
                Model = product.PromoCode,
                Id = product.Id,
                MinPrice = product.MinPrice,
                MaxPrice = product.MaxPrice,
                Category_Id = product.Category_Id,
                FlatPrice = product.FlatPrice,
                IsFlatPrice = product.IsFlatPrice,
                ProductFeatures = _service.ProductFeature.GetProductFeatures().Where(w => w.Product_Id == product.Id).Select(s => new ProductFeatureResponse()
                {
                    FeatureKey = s.FeatureKey,
                    FeatureValue = s.FeatureValue
                }).ToList(),
                ProductImages = new List<FileResponse>(),
                Image = new FileResponse()
                {
                    FileName = product.Image,
                },
                Currency_Id = product.Currency_Id,
                Uom_Id = product.Uom_Id,
                UomName = _service.UOM.GetUOM(product.Uom_Id)?.Title,
                Category = _service.Category.GetCategory(product.Category_Id)?.Name,
                CurrencySymbol = _service.Currency.GetCurrency(product.Currency_Id)?.CurrencySymbol
            };

            if (!string.IsNullOrWhiteSpace(product.Image))
                res.Image.FilePath = FileHelper.GetFileLink(product.Image, FileLinkType.Product);

            var seller = _service.Seller.GetSeller(product.Seller_Id);
            if (seller != null)
                res.Seller = GetSellerResponse(seller);

            var productImages = _service.ProductImage.GetProductImages().Where(f => f.Product_Id == product.Id).ToList();
            foreach (var image in productImages)
            {
                var imageResponse = new FileResponse()
                {
                    Id = image.Id,
                    FileIdentifier = image.FileName,
                    FileName = image.Title,
                };
                if (!string.IsNullOrWhiteSpace(image.FileName))
                    imageResponse.FilePath = FileHelper.GetFileLink(image.FileName, FileLinkType.Product);

                res.ProductImages.Add(imageResponse);
            }

            return res;
        }

        public CategoryResponse GetCategoryResponse(Category item)
        {
            var res = new CategoryResponse()
            {
                Parent_Id = item.Parent_Id,
                Id = item.Id,
                Name = item.Name
            };

            return res;
        }

        public CartResponse GetCartResponse(CartProduct product)
        {
            var item = _service.Product.GetProduct(product.Id);

            var res = new CartResponse()
            {
                TotalQuantity = product.Quantity,
                Product = GetProductResponse(item)
            };

            return res;
        }

        public OrderResponse GetOrderResponse(Order order)
        {
            var res = new OrderResponse()
            {
                CreatedAt = order.CreatedAt,
                Details = new List<OrderDetailResponse>(),
                Id = order.Id,
                InvoiceNumber = order.InvoiceNumber,
                ShippingTerms = order.ShippingTerms,
            };

            var details = _service.OrderDetail.GetOrderDetails().Where(w => w.Order_Id == order.Id);
            foreach (var detail in details)
            {
                res.Details.Add(GetOrderDetailResponse(detail));
            }
            res.TotalAmount = res.Details.Sum(s => s.Price);
            res.TotalItems = res.Details.Sum(s => s.Quantity);

            if (res.Details.Any())
                res.Currency = res.Details.First().Currency;

            return res;
        }

        public OrderDetailResponse GetOrderDetailResponse(OrderDetail detail)
        {
            var res = new OrderDetailResponse()
            {
                Currency_Id = detail.Currency_Id,
                PerUnitPrice = detail.PerUnitPrice,
                Price = detail.Price,
                ProductName = detail.ProductName,
                Quantity = detail.Quantity,
                Title = detail.ProductName,
                UnitOfMasure_Id = detail.UnitOfMeasure_Id,
                UnitOfMasure = _service.UOM.GetUOM(detail.UnitOfMeasure_Id)?.Title,
                Currency = _service.Currency.GetCurrency(detail.Currency_Id)?.Name
            };
            return res;
        }
    }
}