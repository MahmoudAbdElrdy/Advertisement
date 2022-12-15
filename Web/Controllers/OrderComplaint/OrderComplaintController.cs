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
    public partial class OrderComplaintController : BaseController
    {
       
       
        [AllowAnonymous]
        [HttpPost("edit-OrderComplaint")]
        public async Task<ActionResult<OrderComplaintDto>> EditOrderComplaint([FromBody] EditOrderComplaintCommand command)
        {
            return Ok((await Mediator.Send(command)));
        } 
        
        [AllowAnonymous]
        [HttpPost("add-OrderComplaint")]
        public async Task<ActionResult<OrderComplaintDto>> AddOrderComplaint([FromBody] CreateOrderComplaintCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }          
        
        [AllowAnonymous]
        [HttpGet("get-all-OrderComplaint")]
        public async Task<ActionResult<PageList<OrderComplaintDto>>> GetAllOrderComplaint([FromQuery] GetAllOrderComplaintCommand command)
        {   
            return Ok((await Mediator.Send(command)));
        }
        [AllowAnonymous]
        [HttpGet("get-all-OrderComplaint-by-user-id")]
        public async Task<ActionResult<List<OrderComplaintDto>>> GetAllOrderComplaint([FromQuery] GetOrderComplaintByUserIdCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [AllowAnonymous]
        [HttpGet("get-OrderComplaint-detail")]
        public async Task<ActionResult<OrderComplaintDto>>  GetSingleOrderComplaint([FromQuery] GetOrderComplaintCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }
    }
}