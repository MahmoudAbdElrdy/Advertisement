using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApplication.Auth.Commands;
using AuthApplication.Auth.Dto;
using AuthApplication.Auth.Queries;
using AuthDomain.Entities.Auth;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers {
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : BaseController {
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<Result>> Login(
      [FromBody] LoginCommand loginDto
    ) {
      return Ok((await Mediator.Send(loginDto)));
    }

    [HttpPost("change-password")]
    public async Task<ActionResult<Result>> ChangePassword([FromBody] ChangePasswordCommand changePassword) {
      return Ok((await Mediator.Send(changePassword)));
    }

    [AllowAnonymous]
    [HttpPost("forget-password")]
    public async Task<ActionResult<Result>> ForgetPassword([FromBody] ForgetPasswordCommand forgetPassword) {
      return Ok((await Mediator.Send(forgetPassword)));
    }
    [AllowAnonymous]
    [HttpPost("verify-code")]
    public async Task<ActionResult<Result>> VerifyCode([FromBody] VerifyCodeCommand command) {
      return Ok((await Mediator.Send(command)));
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<ActionResult<Result>> ResetPassword([FromBody] ResetPasswordCommand resetPassword) {
      return Ok((await Mediator.Send(resetPassword)));
    }

    [AllowAnonymous]
    [HttpPost("client-register")]
    public async Task<ActionResult<ClientDto>> RegisterClient([FromBody] ClientRegisterCommand command) {
      return Ok(await Mediator.Send(command));
    }
        
    [AllowAnonymous]
    [HttpPost("service-provider-register")]
    public async Task<ActionResult<ClientDto>> RegisterServiceProvider([FromBody] ServiceProviderRegisterCommand command) {
      return Ok(await Mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpPost("Get-User-By-Id")]
    public async Task<ActionResult<ClientDto>> GetUserById([FromBody] GetUserByIdCommand command) {
      return Ok(await Mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpPost("is-user-has-commercial-record")]
    public async Task<ActionResult<bool>> IsUserHasCommercialRecord([FromBody] IsUserHasCommercialRecordCommand command) {
      return Ok(await Mediator.Send(command));
    }

  }
}