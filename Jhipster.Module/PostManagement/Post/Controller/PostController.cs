using System;
using Jhipster.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Post.Application.Commands.BoughtPostC;
using System.Net.NetworkInformation;
using Post.Application.Queries.BoughtPostQ;

namespace Post.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {

        private readonly ILogger<PostController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;

        public PostController(ILogger<PostController> logger, IDistributedCache cache, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Get([FromQuery] string ping)
        {
            _logger.LogInformation($"REST request to ping : {ping}");
            try
            {
                var res = ping;
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private string? GetUserIdFromConext()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private string? GetRoleFromContext()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value;
        }
        [HttpPost("/boughtpost")]
        [AllowAnonymous]
        public async Task<IActionResult> AddBoughtPost([FromBody] AddBoughtPostCommand rq)
        {
            {
                _logger.LogInformation($"REST request to ping : {rq}");
                try
                {
                    var res = rq;
                    return Ok(res);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }

            }
        }
        [HttpPut("/boughtpost")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateBoughtPost([FromBody] UpdateBoughtPostCommand rq)
        {
            {
                _logger.LogInformation($"REST request to ping : {rq}");
                try
                {
                    var res = rq;
                    return Ok(res);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }

            }
        }
        [HttpDelete("/boughtpost/{boughtpost-id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteBoughtPost([FromRoute(Name = "boughtpost-id")] string rq)
        {
            {
                _logger.LogInformation($"REST request to ping : {rq}");
                try
                {
                    var res = new DeleteBoughtPostCommand { Id = rq };
                    return Ok(res);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }

            }
        }
        [HttpPost("/boughtpost/get-all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllBoughtPost([FromBody] ViewAllBoughtPostQuery rq)
        {
            {
                _logger.LogInformation($"REST request to ping : {rq}");
                try
                {
                    var res = rq;
                    return Ok(res);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }

            }
        }
    }
}

