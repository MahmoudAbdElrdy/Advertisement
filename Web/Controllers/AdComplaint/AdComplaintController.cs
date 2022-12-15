using System.Collections.Generic;
using System.Threading.Tasks;
using AuthApplication.Complaint.Command;
using AuthApplication.Complaint.Commands;
using AuthApplication.Complaint.Dto;
using Common.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Complaint
{
    
    [Route("api/[controller]")]
    public partial class AdComplaintController : BaseController
    {
       
       
        [AllowAnonymous]
        [HttpPost("edit-AdComplaint")]
        public async Task<ActionResult<AdComplaintDto>> EditAdComplaint([FromBody] EditAdComplaintCommand command)
        {
            return Ok((await Mediator.Send(command)));
        } 
        
        [AllowAnonymous]
        [HttpPost("add-AdComplaint")]
        public async Task<ActionResult<AdComplaintDto>> AddAdComplaint([FromBody] CreateAdComplaintCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }          
        
        [AllowAnonymous]
        [HttpGet("get-all-AdComplaint")]
        public async Task<ActionResult<PageList<AdComplaintDto>>> GetAllAdComplaint([FromQuery] GetAllAdComplaintCommand command)
        {   
            return Ok((await Mediator.Send(command)));
        }

        [AllowAnonymous]
        [HttpGet("get-AdComplaint-detail")]
        public async Task<ActionResult<AdComplaintDto>>  GetSingleAdComplaint([FromQuery] GetAdComplaintCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }
    }
}