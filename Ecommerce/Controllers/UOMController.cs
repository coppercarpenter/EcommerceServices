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
        public ActionResult<ResponseWrapper<List<UOMResponse>>> GetUOMs()
        {
            return Ok(new ResponseWrapper<List<UOMResponse>>
            {
                Message = MessageHelper.SuccessfullyGet,
                Success = true,
                Data = _service.UOM.GetUOMs().Select(s => new UOMResponse()
                {
                    Id = s.Id,
                    Title = s.Title,
                }).ToList()
            });
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