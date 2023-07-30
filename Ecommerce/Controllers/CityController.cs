using Ecommerce.Attributes;
using Ecommerce.Common;
using Ecommerce.Common.Exceptions;
using Ecommerce.Common.KeysAndValues;
using Ecommerce.Converters;
using Ecommerce.DTO.Models;
using Ecommerce.DTO.Models.Common;
using Ecommerce.Services.Interfaces.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("v1/city")]
    public class CityController : ControllerBase
    {
        #region Private Fields

        private readonly IServiceUnit _service;
        private readonly Converter _converter;

        #endregion Private Fields

        #region Constructors

        public CityController(IServiceUnit service, Converter converter)
        {
            _service = service;
            _converter = converter;
        }

        #endregion Constructors

        #region Methods

        #region End Points

        #region POST

        [HttpPost("")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> AddCity(AddCityRequest model)
        {
            _service.City.AddCity(model.Name, model.Country_Id);

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
        [Route("")]
        public ActionResult<PagedResponse<List<CityResponse>>> GetCities(int? pageSize, int? pageIndex, string name, long? countryId)
        {
            var cities = _service.City.GetCities();

            if (!string.IsNullOrWhiteSpace(name))
                cities = cities.Where(w => w.Name.Contains(name) || w.Name.Equals(name));

            if (countryId.HasValue)
                cities = cities.Where(w => w.Country_Id == countryId.Value);

            var total = 0;
            if (pageIndex.HasValue && pageSize.HasValue && pageSize.Value > 0)
            {
                total = (int)Math.Ceiling(cities.Count() / (double)pageSize.Value);
                cities = cities.Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value);
            }

            cities = cities.OrderBy(o => o.Name);

            var res = new PagedResponse<List<CityResponse>>
            {
                Message = MessageHelper.SuccessfullyGet,
                Success = true,
                Data = new List<CityResponse>(),
                TotalPages = total
            };

            foreach (var item in cities)
            {
                res.Data.Add(_converter.GetCityResponse(item));
            }
            return Ok(res);
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseWrapper<CityResponse>> GetCity(long id)
        {
            var city = _service.City.GetCity(id) ?? throw new NotFoundException("City");

            return Ok(new ResponseWrapper<CityResponse>
            {
                Success = true,
                Message = MessageHelper.SuccessfullyGet,
                Data = _converter.GetCityResponse(city)
            });
        }

        #endregion GET

        #region PUT

        [HttpPut("{id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> EditCity(long id, EditCityRequest model)
        {
            _service.City.EditCity(id, model.Name, model.Country_Id);

            return Ok(new ResponseWrapper<bool>
            {
                Success = true,
                Message = MessageHelper.SuccessfullyUpdated,
                Data = true
            });
        }

        #endregion PUT

        #region DELETE

        [HttpDelete]
        [Route("{id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> DeleteCity(long id)
        {
            return Ok(new ResponseWrapper<bool>()
            {
                Data = _service.City.RemoveCity(id),
                Message = MessageHelper.SuccessfullyDeleted,
                Success = true
            });
        }

        #endregion DELETE

        #endregion End Points

        #endregion Methods
    }
}