using System;
using Jhipster.Crosscutting.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KPI.Controller
{
    [ApiController]
    [Route("gateway/[controller]")]
    [Authorize]
    public class KPIController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public KPIController(IMediator mediator,IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpGet("welcom")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> AddAllCommissionRate()
        {
            try
            {
                return Ok("Hello");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}

