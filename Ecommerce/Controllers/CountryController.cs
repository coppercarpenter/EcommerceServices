using Ecommerce.Attributes;
using Ecommerce.Common;
using Ecommerce.Common.Exceptions;
using Ecommerce.Common.Helpers;
using Ecommerce.Common.KeysAndValues;
using Ecommerce.Converters;
using Ecommerce.DTO.DbModels;
using Ecommerce.DTO.Models;
using Ecommerce.DTO.Models.Common;
using Ecommerce.Services.Interfaces.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("v1/country")]
    public class CountryController : ControllerBase
    {
        #region Private Field

        private readonly IServiceUnit _service;
        private readonly Converter _converter;

        #endregion Private Field

        #region Constructors

        public CountryController(IServiceUnit service, Converter converter)
        {
            _service = service;
            _converter = converter;
        }

        #endregion Constructors

        #region Methods

        #region End Points

        #region POST

        [HttpPost]
        [Route("")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> AddCountry(AddCountryRequest model)
        {
            if (!string.IsNullOrWhiteSpace(model.Flag.FileContent) && !string.IsNullOrWhiteSpace(model.Flag.FileExtension))
                model.Flag.FileIdentifier = FileHelper.UploadFiles(model.Flag.FileContent, model.Flag.FileExtension, FileLinkType.Country);

            _service.Country.AddCountry(model.Name, model.Flag.FileIdentifier);

            return Ok(new ResponseWrapper<bool>
            {
                Success = true,
                Message = MessageHelper.SuccessfullyAdded,
                Data = true
            });
        }

        #endregion POST

        #region GET

        [HttpGet("")]
        public ActionResult<PagedResponse<List<CountryResponse>>> GetAllCountries(int? pageSize, int? pageIndex, string name)
        {
            IQueryable<Country> countries = _service.Country.GetCountries().OrderBy(o => o.Name);

            if (!string.IsNullOrWhiteSpace(name))
                countries = countries.Where(w => w.Name.Contains(name) || w.Name.Equals(name));

            var total = 0;
            if (pageIndex.HasValue && pageSize.HasValue && pageSize.Value > 0)
            {
                total = (int)Math.Ceiling(countries.Count() / (double)pageSize.Value);
                countries = countries.Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value);
            }

            var res = new PagedResponse<List<CountryResponse>>
            {
                Message = MessageHelper.SuccessfullyGet,
                Success = true,
                Data = new List<CountryResponse>(),
                TotalPages = total
            };
            foreach (var item in countries)
            {
                res.Data.Add(_converter.GetCountryResponse(item));
            }
            return Ok(res);
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseWrapper<CountryResponse>> GetCountry(long id)
        {
            var country = _service.Country.GetCountry(id) ?? throw new NotFoundException("Country");
            return Ok(new ResponseWrapper<CountryResponse>
            {
                Success = true,
                Message = MessageHelper.SuccessfullyGet,
                Data = _converter.GetCountryResponse(country)
            });
        }

        [HttpGet]
        [Route("{name}/view")]
        public ActionResult<bool> ViewCountryImage(string name)
        {
            var basePath = AppSettingHelper.GetCountryPath();

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

        [HttpPut("{id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<ResponseWrapper<CountryResponse>> EditCountry(long id, EditCountryRequest model)
        {
            if (!string.IsNullOrWhiteSpace(model.Flag.FileContent) && !string.IsNullOrWhiteSpace(model.Flag.FileExtension))
                model.Flag.FileIdentifier = FileHelper.UploadFiles(model.Flag.FileContent, model.Flag.FileExtension, FileLinkType.Country);

            _service.Country.EditCountry(id, model.Name, model.Flag.FileIdentifier);

            return Ok(new ResponseWrapper<bool>
            {
                Success = true,
                Message = MessageHelper.SuccessfullyUpdated,
                Data = true
            });
        }

        #endregion PUT

        #region DELETE

        [HttpDelete("{id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> DeleteCountry(long id)
        {
            return Ok(new ResponseWrapper<bool>()
            {
                Data = _service.Country.RemoveCountry(id),
                Message = MessageHelper.SuccessfullyDeleted,
                Success = true
            });
        }

        #endregion DELETE

        #endregion End Points

        #endregion Methods
    }
}