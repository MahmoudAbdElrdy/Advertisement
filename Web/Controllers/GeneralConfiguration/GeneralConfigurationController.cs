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
    public partial class GeneralConfigurationController : BaseController
    {
       
       
        [AllowAnonymous]
        [HttpPost("edit-GeneralConfiguration")]
        public async Task<ActionResult<GeneralConfigurationDto>> EditGeneralConfiguration([FromBody] EditGeneralConfigurationCommand command)
        {
            return Ok((await Mediator.Send(command)));
        } 
        
        [AllowAnonymous]
        [HttpPost("add-GeneralConfiguration")]
        public async Task<ActionResult<GeneralConfigurationDto>> AddGeneralConfiguration([FromBody] CreateGeneralConfigurationCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }          
        
        [AllowAnonymous]
        [HttpGet("get-all-GeneralConfiguration")]
        public async Task<ActionResult<PageList<GeneralConfigurationDto>>> GetAllGeneralConfiguration([FromQuery] GetAllGeneralConfigurationCommand command)
        {   
            return Ok((await Mediator.Send(command)));
        }       

        [AllowAnonymous]
        [HttpGet("get-GeneralConfiguration-detail")]
        public async Task<ActionResult<GeneralConfigurationDto>>  GetSingleGeneralConfiguration([FromQuery] GetGeneralConfigurationCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }
    }
}