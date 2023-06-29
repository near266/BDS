using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Post.Application.Commands.NotificationC;
using Post.Application.Queries.NotificationQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Post.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NotificationController> _logger;
        public NotificationController(IMediator mediator, ILogger<NotificationController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet("id")]
        public async Task<IActionResult> ViewNotification([FromQuery] GetAllNotificationsQuery rq)
        {
            _logger.LogInformation($"REST view notification : {rq}");
            try
            {
                var value = await _mediator.Send(rq);
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"fail to view notification {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateNotification([FromBody] UpdateNotificationCommand rq)
        {
            _logger.LogInformation($"update seen notification ");
            try
            {
                var value = await _mediator.Send(rq);
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"fail to update seen notification {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
