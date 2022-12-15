using System.Collections.Generic;
using System.Threading.Tasks;
using AuthApplication.Notifications.Command;
using AuthApplication.Notifications.Dto;
using Common;
using Common.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Notification
{

    [Route("api/[controller]")]
    public partial class NotificationController : BaseController
    {
     
        [AllowAnonymous]
        [HttpGet("get-all-notification")]
        public async Task<ActionResult<PageList<NotificationDto>>> GetAllNotification([FromQuery] GetAllNotificationCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }
        [AllowAnonymous]
        [HttpGet("Read")]
        public async Task<ActionResult<Result>> Read([FromQuery] ReadCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }
        [AllowAnonymous]
        [HttpGet("NotificationCount")]
        public async Task<ActionResult<Result>> NotificationCount([FromQuery] NotificationCountCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }
        [AllowAnonymous]
        [HttpGet("get-all-notification-for-admin")]
        public async Task<ActionResult<PageList<NotificationDto>>> GetAllNotification([FromQuery] GetNotificationForAdminCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }
    }
}