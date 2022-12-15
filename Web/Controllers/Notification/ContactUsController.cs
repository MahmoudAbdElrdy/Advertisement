using System.Collections.Generic;
using System.Threading.Tasks;
using AuthApplication.Auth.Commands;
using AuthApplication.Auth.Queries;
using AuthApplication.ContactUs.Command;
using AuthApplication.Notification.Command;
using AuthApplication.Notification.Dto;
using AuthApplication.Regions.Commands;
using AuthApplication.Regions.Dto;
using AuthApplication.Regions.Queries;
using AuthApplication.UserManagment.Commands;
using AuthApplication.UserManagment.Queries;
using Common.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.ContactUs
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ContactUsController : BaseController
    {
        [AllowAnonymous]
        [HttpGet("ContactUs")]
        public async Task<ActionResult<PageList<ContactUsDto>>> GetContactUs([FromQuery] GetAllContactUsCommand query)
        {
            return Ok((await Mediator.Send(query)));
        }

       

        [AllowAnonymous]
        [HttpPost("ContactUs/add-ContactUs")]
        public async Task<ActionResult<PageList<PageList<ContactUsDto>>>> AddContactUs([FromBody] AddContactUsCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [HttpPut("ContactUs/edit-ContactUs/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<ContactUsDto>>> EditContactUs(
          [FromRoute] string id,
          [FromBody] EditContactUsCommand command
        )
        {
            command.Id = id;
            return Ok((await Mediator.Send(command)));
        }

        [HttpDelete("ContactUs/delete-ContactUs/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<ContactUsDto>>> DeleteContactUs([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new DeleteContactUsCommand() { Id = id }));
        }


    }
}
