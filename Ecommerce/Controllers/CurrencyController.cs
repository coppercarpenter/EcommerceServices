using Ecommerce.Attributes;
using Ecommerce.Common;
using Ecommerce.Common.KeysAndValues;
using Ecommerce.DTO.Models;
using Ecommerce.DTO.Models.Common;
using Ecommerce.Services.Interfaces.Unit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("v1/currency")]
    public class CurrencyController : ControllerBase
    {
        #region Private Fields

        private readonly IServiceUnit _service;

        #endregion Private Fields

        #region Constructors

        public CurrencyController(IServiceUnit service)
        {
            _service = service;
        }

        #endregion Constructors

        #region Methods

        #region End Points

        #region POST

        [HttpPost("")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> AddCurrency(AddCurrencyRequest model)
        {
            _service.Currency.AddCurrency(model.Name, model.Symbol);

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
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin, AccountType.Seller })]
        public ActionResult<PagedResponse<List<CurrencyResponse>>> GetCurrencies(int? pageSize, int? pageIndex)
        {
            var currencies = _service.Currency.GetCurrencies();

            var res = new PagedResponse<List<CurrencyResponse>>
            {
                Message = MessageHelper.SuccessfullyGet,
                Success = true,
                PageNumber = pageIndex.GetValueOrDefault(),
                PageSize = pageSize.GetValueOrDefault(),
                TotalRecords = currencies.Count(),
                Data = new List<CurrencyResponse>()
            };



            if (pageIndex.HasValue && pageSize.HasValue && pageSize.Value > 0)
            {
                res.TotalPages = (int)Math.Ceiling(currencies.Count() / (double)pageSize.Value);
                currencies = currencies.Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value);
            }
            res.Data = currencies.Select(s => new CurrencyResponse()
            {
                Id = s.Id,
                Name = s.Name,
                Symbol = s.CurrencySymbol
            }).ToList();

            return Ok(res);
        }

        #endregion GET

        #region PUT

        [HttpPut("{id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> EditCurrency(long id, EditCurrencyRequest model)
        {
            _service.Currency.EditCurrency(id, model.Name, model.Symbol);

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
        public ActionResult<bool> RemoveCurrency(long id)
        {
            _service.Currency.RemoveCurrency(id);

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