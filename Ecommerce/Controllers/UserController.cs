using Ecommerce.Attributes;
using Ecommerce.Common;
using Ecommerce.Common.KeysAndValues;
using Ecommerce.Converters;
using Ecommerce.DTO.Models;
using Ecommerce.DTO.Models.Common;
using Ecommerce.Services.Interfaces.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("v1/user")]
    public class UserController : ControllerBase
    {
        #region Private Fields

        private readonly IServiceUnit _service;
        private readonly Converter _converter;

        #endregion Private Fields

        #region Constructors

        public UserController(IServiceUnit service, Converter converter)
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
        public ActionResult<ResponseWrapper<bool>> AddUserAccount(AddUserRequest model)
        {
            _service.User.AddUser(model.Username, model.Email, model.Password, model.PhoneNumber);

            return Ok(new ResponseWrapper<bool>()
            {
                Success = true,
                Message = MessageHelper.SuccessfullyAdded,
                Data = true
            });
        }

        #endregion POST

        #region GET

        [HttpGet("")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<PagedResponse<List<UserResponse>>> GetAll(int? pageSize, int? pageIndex)
        {
            var users = _service.User.GetUsers();

            var total = 0;

            if (pageIndex.HasValue && pageSize.HasValue && pageSize.Value > 0)
            {
                total = (users.Count() + pageSize.Value) / pageSize.Value;
                users = users.Skip(pageSize.Value * pageIndex.Value).Take(pageSize.Value);
            }

            var res = new PagedResponse<List<UserResponse>>
            {
                Message = MessageHelper.SuccessfullyGet,
                Success = true,
                Data = new List<UserResponse>(),
                TotalPages = total
            };
            foreach (var item in users)
            {
                res.Data.Add(_converter.GetUserResponse(item));
            }

            return Ok(res);
        }

        #endregion GET

        #region PUT

        [HttpPut("{id}")]
        [CheckJwt(Allows = new AccountType[] { AccountType.Admin })]
        public ActionResult<ResponseWrapper<bool>> EditUserAccount(long id, EditUserRequest model)
        {
            var updatedUser = _service.User.EditUser(id, model.Username, model.Email, model.PhoneNumber);

            return Ok(new ResponseWrapper<bool>()
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
        public ActionResult<ResponseWrapper<bool>> RemoveUserAccount(long id)
        {
            return Ok(new ResponseWrapper<bool>()
            {
                Success = true,
                Message = MessageHelper.SuccessfullyDeleted,
                Data = _service.User.RemoveUser(id)
            });
        }

        #endregion DELETE

        #endregion End Points

        #endregion Methods
    }
}