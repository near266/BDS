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
using Post.Application.Commands.SalePostC;
using Post.Application.Queries.SalePostQ;
using MediatR;
using Jhipster.Crosscutting.Constants;
using Post.Application.Commands.AdminC;

namespace Post.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {

        private readonly ILogger<PostController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IDistributedCache _cache;

        public PostController(ILogger<PostController> logger, IDistributedCache cache, IConfiguration configuration, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet("ping")]
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

        private bool CheckRoleList(string? roles, string item)
        {
            if (roles.Contains(item)) return true;
            else return false;
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
            return User.FindFirst(ClaimsTypeConst.Username)?.Value;
        }

        [HttpPost("/boughtpost/add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddBoughtPost([FromBody] AddBoughtPostCommand rq)
        {
            _logger.LogInformation($"REST request to add bought post : {rq}");
            try
            {
                rq.Username = GetUsernameFromContext();
                rq.UserId = GetUserIdFromConext();
                rq.CreatedDate = DateTime.UtcNow;
                rq.CreatedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add bought post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("/boughtpost/update")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateBoughtPost([FromBody] UpdateBoughtPostCommand rq)
        {

            _logger.LogInformation($"REST request to update bought post : {rq}");
            try
            {
                rq.LastModifiedDate = DateTime.UtcNow;
                rq.LastModifiedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update bought post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }
        [HttpDelete("/boughtpost/delete")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteBoughtPost([FromQuery] string rq)
        {

            _logger.LogInformation($"REST request to delete bought post : {rq}");
            try
            {
                var res = new DeleteBoughtPostCommand { Id = rq };
                var result = await _mediator.Send(res);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete bought post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }
        [HttpPost("/boughtpost/search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchBoughtPost([FromBody] ViewAllBoughtPostQuery rq)
        {
            _logger.LogInformation($"REST request to search bought post : {rq}");
            try
            {
                var role = GetRoleFromContext();
                if (!CheckRoleList(role, RolesConstants.ADMIN))
                {
                    rq.UserId = GetUserIdFromConext();
                }
                else
                {
                    rq.UserId = null;
                }
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search bought post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/boughtpost/getShowing")]
        [AllowAnonymous]
        public async Task<IActionResult> GetShowingBoughtPost([FromBody]GetAllShowingBoughtPostQuery rq)
        {
            _logger.LogInformation($"REST request to get showing bought post");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get showing bought post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("/boughtpost/id")]
        [AllowAnonymous]
        public async Task<IActionResult> ViewDetailBoughtPost([FromQuery] ViewDetailBoughtPostQuery rq)
        {

            _logger.LogInformation($"REST request to view detail bought post : {rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to view detail bought post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }
        [HttpPost("/salepost/add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddSalePost([FromBody] AddSalePostCommand rq)
        {

            _logger.LogInformation($"REST request to add sale post : {rq}");
            try
            {
                rq.Username = GetUsernameFromContext();
                rq.UserId = GetUserIdFromConext();
                rq.CreatedDate = DateTime.UtcNow;
                rq.CreatedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add sale post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }
        [HttpPut("/salepost/update")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateSalePost([FromBody] UpdateSalePostCommand rq)
        {

            _logger.LogInformation($"REST request to update sale post : {rq}");
            try
            {
                rq.LastModifiedDate = DateTime.UtcNow;
                rq.LastModifiedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update sale post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }
        [HttpDelete("/salepost/delete")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteSalePost([FromQuery] string rq)
        {

            _logger.LogInformation($"REST request to delete sale post : {rq}");
            try
            {
                var res = new DeleteSalePostCommand { Id = rq };
                var result = await _mediator.Send(res);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete sale post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }
        [HttpPost("/salepost/search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchSalePost([FromBody] ViewAllSalePostQuery rq)
        {
            _logger.LogInformation($"REST request to search sale post : {rq}");
            try
            {
                var role = GetRoleFromContext();
                if (!CheckRoleList(role, RolesConstants.ADMIN))
                {
                    rq.UserId = GetUserIdFromConext();
                }
                else
                {
                    rq.UserId = null;
                }
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search sale post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/salepost/getShowing")]
        public async Task<IActionResult> GetShowingSaletPost([FromBody]GetAllShowingSalePostQuery rq)
        {
            _logger.LogInformation($"REST request to get showing sale post");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get showing sale post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/salepost/id")]
        [AllowAnonymous]
        public async Task<IActionResult> ViewDetailSalePost([FromQuery] ViewDetailSalePostQuery rq)
        {

            _logger.LogInformation($"REST request to view detail : {rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to view detail fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }

        [HttpPost("/admin/approve")]
        [AllowAnonymous]
        public async Task<IActionResult> ApprovePost([FromBody] ApprovePostCommand rq)
        {
            _logger.LogInformation($"REST request to approve post : {rq}");
            try
            {
                rq.modifiedDate = DateTime.UtcNow;
                rq.modifiedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to approve post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}

