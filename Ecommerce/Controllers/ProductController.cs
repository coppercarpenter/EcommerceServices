using Ecommerce.Attributes;
using Ecommerce.Common;
using Ecommerce.Common.Exceptions;
using Ecommerce.Common.Helpers;
using Ecommerce.Common.KeysAndValues;
using Ecommerce.Converters;
using Ecommerce.DTO.Models;
using Ecommerce.DTO.Models.Common;
using Ecommerce.Services.Interfaces.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Private Fields

        private readonly IServiceUnit _service;
        private readonly Converter _converter;

        #endregion Private Fields

        #region Constructors

        public ProductController(IServiceUnit service, Converter converter)
        {
            _service = service;
            _converter = converter;
        }

        #endregion Constructors

        #region Methods

        #region End Points

        #region POST

        [HttpPost("")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Seller, AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> AddProduct(AddProductRequest model)
        {
            var token = _converter.GetAdminToken(HttpContext);

            switch (token.Type)
            {
                case AccountType.Seller:
                    model.Seller_Id = token.Id;
                    break;
                default:
                    if (!model.Seller_Id.HasValue)
                        throw new BadRequestException("Seller is Required");
                    break;
            }

            if (!string.IsNullOrWhiteSpace(model.Image.FileContent) && !string.IsNullOrWhiteSpace(model.Image.FileExtension))
                model.Image.FileIdentifier = FileHelper.UploadFiles(model.Image.FileContent, model.Image.FileExtension, FileLinkType.Product);

            var product = _service.Product.AddProduct(model.Title, model.Model, model.Description, model.PromoCode,
                                                      model.Seller_Id.Value, model.Category_Id, model.IsFlatPrice,
                                                      model.MinPrice, model.MaxPrice, model.FlatPrice, model.Uow_Id,
                                                      model.Keyword, model.Currency_Id, model.Image.FileIdentifier);

            foreach (var item in model.ProductFeatures)
            {
                _service.ProductFeature.AddProductFeature(product.Id, item.FeatureKey, item.FeatureValue);
            }

            foreach (var item in model.ProductImages)
            {
                if (!string.IsNullOrWhiteSpace(item.FileContent) && !string.IsNullOrWhiteSpace(item.FileExtension))
                    item.FileIdentifier = FileHelper.UploadFiles(item.FileContent, item.FileExtension, FileLinkType.Product);
                _service.ProductImage.AddProductImage(product.Id, item.FileName, item.FileIdentifier);
            }

            return Ok(new ResponseWrapper<bool>()
            {
                Data = true,
                Message = MessageHelper.SuccessfullyAdded,
                Success = true
            });
        }

        #endregion POST

        #region GET

        [HttpGet]
        [Route("")]
        public ActionResult<PagedResponse<List<ProductResponse>>> GetAllProducts(long? category_id, string name,
                                                                                 bool? ispublished, long? categoryId, int? pageSize,
                                                                                 int? pageIndex, long? seller_id,
                                                                                 bool? isFeature, DateTime? createdDate,
                                                                                 DateTime? modifiedDate)
        {
            var res = new PagedResponse<List<ProductResponse>>
            {
                Message = MessageHelper.SuccessfullyGet,
                Success = true,
                Data = new List<ProductResponse>(),
            };

            var products = _service.Product.GetProducts();

            if (category_id.HasValue)
                products = products.Where(w => w.Category_Id == category_id.Value);

            if (!string.IsNullOrWhiteSpace(name))
                products = products.Where(w => w.Title.Contains(name));

            if (seller_id.HasValue && seller_id.Value > 0)
                products = products.Where(w => w.Seller_Id == seller_id.Value);

            if (pageIndex.HasValue && pageSize.HasValue && pageSize.Value > 0)
            {
                res.TotalPages = (int)Math.Ceiling(products.Count() / (double)pageSize.Value);
                products = products.Skip(pageSize.Value * pageIndex.Value).Take(pageSize.Value);
            }

            foreach (var product in products)
            {
                res.Data.Add(_converter.GetProductResponse(product));
            }

            return Ok(res);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ResponseWrapper<ProductResponse>> GetProduct(long id)
        {
            var product = _service.Product.GetProduct(id) ?? throw new NotFoundException("Product");

            return Ok(new ResponseWrapper<ProductResponse>()
            {
                Data = _converter.GetProductResponse(product),
                Message = MessageHelper.SuccessfullyGet,
                Success = true
            });
        }

        [HttpGet("{name}/view")]
        public ActionResult<bool> GetProductImage(string name)
        {
            var basePath = AppSettingHelper.GetProductPath();

            if (System.IO.File.Exists(basePath + name))
            {
                var fileExtension = Path.GetExtension(basePath + name);

                var openFile = System.IO.File.OpenRead(basePath + name);

                return File(openFile, MimeTypeMap.GetMimeType(fileExtension));
            }
            else
            {
                return NotFound(new ResponseWrapper<object>(false, MessageHelper.DataNotFound, null, null));
            }
        }

        #endregion GET

        #region PUT

        [HttpPut]
        [Route("{id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Seller, AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> EditProduct(long id, EditProductRequest model)
        {
            _service.BeginTransaction();

            var features = _service.ProductFeature.GetProductFeatures().Where(w => w.Product_Id == id).ToList();
            foreach (var feature in features)
            {
                _service.ProductFeature.RemoveProductFeature(feature.Id);
            }

            var images = _service.ProductImage.GetProductImages().Where(w => w.Product_Id == id).ToList();
            foreach (var image in images)
            {
                _service.ProductImage.RemoveProductImage(image.Id);
            }

            if (!string.IsNullOrWhiteSpace(model.Image.FileContent) && !string.IsNullOrWhiteSpace(model.Image.FileExtension))
                model.Image.FileIdentifier = FileHelper.UploadFiles(model.Image.FileContent, model.Image.FileExtension, FileLinkType.Product);

            var product = _service.Product.EditProduct(id, model.Title, model.Model, model.Description,
                                                       model.PromoCode, model.Category_Id,
                                                       model.IsFlatPrice, model.MinPrice,
                                                       model.MaxPrice, model.FlatPrice, model.Uom_Id, model.Keyword,
                                                       model.Currency_Id, model.Image.FileIdentifier);

            foreach (var item in model.ProductFeatures)
            {
                _service.ProductFeature.AddProductFeature(id, item.FeatureKey, item.FeatureValue);
            }

            foreach (var item in model.ProductImages)
            {
                if (!string.IsNullOrWhiteSpace(item.FileContent) && !string.IsNullOrWhiteSpace(item.FileExtension))
                    item.FileIdentifier = FileHelper.UploadFiles(item.FileContent, item.FileExtension, FileLinkType.Product);
                _service.ProductImage.AddProductImage(id, item.FileName, item.FileIdentifier);
            }

            _service.CommitTransaction();

            return Ok(new ResponseWrapper<bool>()
            {
                Data = true,
                Message = MessageHelper.SuccessfullyUpdated,
                Success = true
            });
        }

        #endregion PUT

        #region DELETE

        [HttpDelete]
        [Route("{id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Seller, AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> RemoveDelete(long id)
        {
            _service.BeginTransaction();

            _service.Product.RemoveProduct(id);

            _service.CommitTransaction();

            return Ok(new ResponseWrapper<bool>()
            {
                Data = true,
                Message = MessageHelper.SuccessfullyDeleted,
                Success = true
            });
        }

        #endregion DELETE

        #endregion End Points

        #endregion Methods
    }
}