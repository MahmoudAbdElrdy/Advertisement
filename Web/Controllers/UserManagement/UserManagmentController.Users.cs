using System.Threading.Tasks;
using AuthApplication.UserManagment.Commands;
using AuthApplication.UserManagment.Dto;
using AuthApplication.UserManagment.Queries;
using Common.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.UserManagement
{
    public partial class UserManagementController
    {

        [HttpGet("users/getallusers")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<UserDto>>> GetAll([FromQuery] GetUserListQuery query)
        {
            return Ok((await Mediator.Send(query)));
        }

        [HttpPost("users")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<UserDto>>> AddUser([FromBody] AddUserCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }


        [HttpPut("users/edit-user/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<UserDto>>> EditUser(
            [FromRoute] string id,
            [FromBody] EditUserCommand command
          )
        {
            command.Id = id;
            return Ok((await Mediator.Send(command)));
        }
        [HttpPut("users/SetToken")]
        [AllowAnonymous]
        public async Task<ActionResult> SetToken([FromBody] SetTokenCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPut("users/SetLang")]
        [AllowAnonymous]
        public async Task<ActionResult> SetLang([FromBody] SetLangCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("users/delete-user/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<UserDto>>> DeleteUser([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand() { Id = id }));
        }
        [HttpGet("users/Get/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Get([FromRoute] string id)
        {
            return Ok((await Mediator.Send(new GetUserCommand() { Id = id })));
        }


    }
}