using Jhipster.Crosscutting.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Post.Application.Commands.FakeNewC;
using Post.Application.Commands.NotificationC;
using Post.Application.Queries.FakeNewQ;
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
        private string? GetUserIdFromConext()
        {
            return User.FindFirst(ClaimsTypeConst.Id)?.Value;
        }

        private string? GetRoleFromContext()
        {
            return User.FindFirst(ClaimsTypeConst.Role)?.Value;
        }
        private string? GetUsernameFromContext()
        {
            return User.FindFirst(ClaimsTypeConst.Name)?.Value;
        }
        [HttpGet("id")]
        public async Task<IActionResult> ViewNotification([FromQuery] GetAllNotificationsQuery rq)
        {
            _logger.LogInformation($"REST view notification : {rq}");
            try
            {
                rq.Id = GetUserIdFromConext();
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
        [HttpGet("FakeNew")]
        public async Task<IActionResult> ViewFakeNew()
        {
            var rq = new ViewFakeNewQuery();
            var value = await _mediator.Send(rq);
            var reponse = new ViewFakeNewDTO();
            reponse.data = value;
            return Ok(reponse);
        }
        public class ViewFakeNewDTO
        {
            public string data { get; set; }
        }
        [HttpDelete("FakeNew/id")]
        public async Task<IActionResult> DeleteFakeNew([FromBody] DeleteFakeNewCommand rq)
        {
            return Ok(await _mediator.Send(rq));
        }
        [HttpPost("FakeNew/add")]
        public async Task<IActionResult> AddFakeNew([FromBody] AddFakeNewCommand rq)
        {
            return Ok(await _mediator.Send(rq));
        }
        [HttpPut("FakeNew/update")]
        public async Task<IActionResult> UpdateFakeNew([FromBody] UpdateFakeNewCommand rq)
        {
            return Ok(await _mediator.Send(rq));
        }
        [HttpGet("FakeNew/viewall")]
        public async Task<IActionResult> GetAllFake([FromQuery] ViewAllFakeQuery rq)
        {
            return Ok(await _mediator.Send(rq));
        }
    }
}
