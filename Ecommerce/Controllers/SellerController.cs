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
    [ApiController]
    [Route("v1/seller")]
    public class SellerController : ControllerBase
    {
        #region Private Fields

        private readonly IServiceUnit _service;
        private readonly Converter _converter;

        #endregion Private Fields

        #region Constructors

        public SellerController(IServiceUnit service, Converter converter)
        {
            _service = service;
            _converter = converter;
        }

        #endregion Constructors

        #region Methods

        #region End Points

        #region POST

        [HttpPost]
        [Route("add_user")]
        public ActionResult<ResponseWrapper<bool>> AddUser(AddSellerRequest model)
        {
            _service.Seller.AddSeller(model.Username, model.Password, model.FirstName, model.LastName, model.Email,
                                      model.Website, model.CompanyAddres, model.Fax, model.City_Id);

            return Ok(new ResponseWrapper<bool>
            {
                Success = true,
                Message = MessageHelper.SuccessfullyAdded,
                Data = true
            });
        }

        #endregion POST

        #region GET

        [HttpGet]
        [Route("{name}/view")]
        public ActionResult<bool> ViewSellerImage(string name)
        {
            var basePath = AppSettingHelper.GetSellerPath();

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

        [HttpGet]
        [Route("")]
        public ActionResult<PagedResponse<List<SellerResponse>>> GetAllSellers(int? pageSize, int? pageIndex,
                                                                               string email, long? cityId)
        {
            var sellers = _service.Seller.GetSellers();

            if (!string.IsNullOrWhiteSpace(email))
                sellers = sellers.Where(w => w.Email.Equals(email) || w.Email.Contains(email));

            if (cityId.HasValue)
                sellers = sellers.Where(w => w.City_Id == cityId.Value);

            var total = 0;
            if (pageIndex.HasValue && pageSize.HasValue && pageSize.Value > 0)
            {
                total = (int)Math.Ceiling(sellers.Count() / (double)pageSize.Value);
                sellers = sellers.Skip(pageSize.Value * pageIndex.Value).Take(pageSize.Value);
            }

            var res = new PagedResponse<List<SellerResponse>>
            {
                Message = MessageHelper.SuccessfullyGet,
                Success = true,
                Data = new List<SellerResponse>(),
                TotalPages = total
            };

            foreach (var item in sellers)
            {
                res.Data.Add(_converter.GetSellerResponse(item));
            }
            return Ok(res);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ResponseWrapper<SellerResponse>> GetSeller(long id)
        {
            var seller = _service.Seller.GetSeller(id) ??
                throw new NotFoundException("Seller");

            return Ok(new ResponseWrapper<SellerResponse>
            {
                Message = MessageHelper.SuccessfullyGet,
                Success = true,
                Data = _converter.GetSellerResponse(seller)
            });
        }

        #endregion GET

        #region PUT

        [HttpPut("{id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Seller, AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> EditUserSeller(long id, EditSellerRequest model)
        {
            if (!string.IsNullOrWhiteSpace(model.Image.FileContent) && !string.IsNullOrWhiteSpace(model.Image.FileExtension))
                model.Image.FileIdentifier = FileHelper.UploadFiles(model.Image.FileContent, model.Image.FileExtension, FileLinkType.Seller);

            _service.Seller.EditSeller(id, model.Username, model.FirstName, model.LastName, model.Email,
                                       model.Image.FileIdentifier, model.Website, model.CompanyAddress, model.Fax,
                                       model.City_Id);

            return Ok(new ResponseWrapper<bool>()
            {
                Data = true,
                Success = true,
                Message = MessageHelper.SuccessfullyUpdated
            });
        }

        #endregion PUT

        #region DELETE

        [HttpDelete("{id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> RemoveSeller(long id)
        {
            _service.Seller.RemoveSeller(id);

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