using System.Collections.Generic;
using System.Threading.Tasks;
using AuthApplication.Advertisement.Queries;
using AuthApplication.LookUp.Commands;
using AuthApplication.LookUp.Dto;
using AuthApplication.LookUp.Queries;
using AuthApplication.Regions.Commands;
using AuthApplication.Regions.Dto;
using AuthApplication.Regions.Queries;
using AuthApplication.UserManagment.Commands;
using AuthApplication.UserManagment.Queries;
using Common.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.UserManagement
{
    public partial class ServiceManagementController : BaseController
    {
        
        [AllowAnonymous]
        [HttpGet("get-services-List-by-type-id")]
        public async Task<ActionResult<List<ServiceDto>>> GetServicesByServiceTypeId([FromQuery] GetAllServicesByServiceTypeIdCommand query)
        {
            return Ok((await Mediator.Send(query)));
        }
         
        [AllowAnonymous]
        [HttpGet("get-service-types-List")]
        public async Task<ActionResult<List<ServiceTypeDto>>> GetServiceTypes([FromQuery] GetServiceTypeListCommand query)
        {
            return Ok((await Mediator.Send(query)));
        }
        [AllowAnonymous]
        [HttpGet("get-request-types-List")]
        public async Task<ActionResult<List<ServiceTypeDto>>> GetRequestTypeListCommand([FromQuery] GetRequestTypeListCommand query)
        {
            return Ok((await Mediator.Send(query)));
        }
        [AllowAnonymous]
        [HttpGet("get-service")]
        public async Task<ActionResult<PageList<ServiceTypeDto>>> GetServiceTypes([FromQuery] GetAllServiceWithPaginationCommand query)
        {
            return Ok((await Mediator.Send(query)));
        }

        [AllowAnonymous]
        [HttpGet("Services")]
        public async Task<ActionResult<PageList<ServiceTypeDto>>> GetServices([FromQuery] GetAllServicesWithPaginationCommand query)
        {
            return Ok((await Mediator.Send(query)));
        }

        [AllowAnonymous]
        [HttpPost("Services/add-Service")]
        public async Task<ActionResult<PageList<ServiceDto>>> AddService([FromBody] CreateServiceCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [HttpPut("Services/edit-Service/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<ServiceDto>>> EditService(
       [FromRoute] string id,
       [FromBody] EditServiceCommand command
     )
        {
            command.Id = id;
            return Ok((await Mediator.Send(command)));
        }

      
        [AllowAnonymous]
        [HttpDelete("Services/delete-Service/{id}")]
        public async Task<ActionResult<PageList<ServiceDto>>> DeleteService([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new DeleteServiceCommand() { Id = id }));
        } 
    }
}