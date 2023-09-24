using Ecommerce.Attributes;
using Ecommerce.Common;
using Ecommerce.Common.KeysAndValues;
using Ecommerce.DTO.Models;
using Ecommerce.DTO.Models.Common;
using Ecommerce.Services.Interfaces.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("v1/uom")]
    public class UOMController : ControllerBase
    {
        #region Private Fields

        private readonly IServiceUnit _service;

        #endregion Private Fields

        #region Constructors

        public UOMController(IServiceUnit service)
        {
            _service = service;
        }

        #endregion Constructors

        #region Methods

        #region End Points

        #region POST

        [HttpPost("")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> AddUOM(AddUOMRequest model)
        {
            _service.UOM.AddUOM(model.Title);

            var res = new ResponseWrapper<bool>
            {
                Success = true,
                Message = MessageHelper.SuccessfullyAdded,
                Data = true
            };

            return Ok(res);
        }

        #endregion POST

        #region GET

        [HttpGet]
        [Route("")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin, AccountType.Seller })]
        public ActionResult<PagedResponse<List<UOMResponse>>> GetUOMs(int? pageSize, int? pageIndex)
        {
            var uoms = _service.UOM.GetUOMs();

            var res = new PagedResponse<List<UOMResponse>>
            {
                Message = MessageHelper.SuccessfullyGet,
                Success = true,
                PageNumber = pageIndex.GetValueOrDefault(),
                PageSize = pageSize.GetValueOrDefault(),
                TotalRecords = uoms.Count(),
                Data = new List<UOMResponse>()
            };
            if (pageIndex.HasValue && pageSize.HasValue && pageSize.Value > 0)
            {
                res.TotalPages = (int)Math.Ceiling(uoms.Count() / (double)pageSize.Value);
                uoms = uoms.Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value);
            }

            res.Data = uoms.Select(s => new UOMResponse()
            {
                Id = s.Id,
                Title = s.Title,
            }).ToList();

            return Ok(res);
        }

        #endregion GET

        #region DELETE

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<bool> RemoveUOM(long id)
        {
            return Ok(new ResponseWrapper<bool>()
            {
                Data = _service.UOM.RemoveUOM(id),
                Message = MessageHelper.SuccessfullyDeleted,
                Success = true
            });
        }

        #endregion DELETE

        #endregion End Points

        #endregion Methods
    }
}