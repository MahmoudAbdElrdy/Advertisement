using System.Threading.Tasks;
using AuthApplication.Auth.Commands;
using AuthApplication.Auth.Queries;
using AuthApplication.UserManagment.Commands;
using AuthApplication.UserManagment.Dto;
using AuthApplication.UserManagment.Queries;
using Common.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.UserManagement {
  public partial class UserManagementController {
    [HttpGet("roles")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<RoleDto>>> GetRoles(
      [FromQuery] GetRoleWithPagination query
    ) {
      return Ok((await Mediator.Send(query)));
    }

    [HttpPost("roles/add-role")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<RoleDto>>> AddRole([FromBody] AddRoleCommand command) {
      return Ok((await Mediator.Send(command)));
    }

    [HttpPut("roles/edit-role/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<RoleDto>>> EditRole(
      [FromRoute] string id,
      [FromBody] EditRoleCommand command
    ) {
      command.Id = id;
      return Ok((await Mediator.Send(command)));
    }

    [HttpDelete("roles/delete-role/{id}")]
    public async Task<ActionResult<PageList<RoleDto>>> DeleteRole([FromRoute] string id) {
      return Ok(await Mediator.Send(new DeleteRoleCommand() {Id = id}));
    }

    [HttpGet("permissions")]
    public async Task<ActionResult<PageList<RoleDto>>> GetPermission() {
        return Ok((await Mediator.Send(new GetAllPermisionQuery())));
    }

    [HttpPost("add-permission")]
    public async Task<ActionResult<PageList<RoleDto>>> AddPermissionToRole([FromBody] AddPermissionToRolCommand command) {
        return Ok((await Mediator.Send(command)));
    }


    }
}