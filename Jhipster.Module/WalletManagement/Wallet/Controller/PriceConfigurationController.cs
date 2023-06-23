using Jhipster.Crosscutting.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Commands.TypePriceC;

namespace Wallet.Controller
{
    
    [ApiController]
    [Route("[controller]")]
    public class PriceConfigurationController : ControllerBase
    {
        private readonly ILogger<PriceConfigurationController> _logger;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public PriceConfigurationController(ILogger<PriceConfigurationController> logger, IDistributedCache cache, IConfiguration configuration, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
            return User.FindFirst(ClaimsTypeConst.Name)?.Value;
        }

        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPost("/typeprice/add")]
        public async Task<IActionResult> AddTypePrice([FromBody] AddTypePriceCommand rq)
        {
            _logger.LogInformation($"Rest request to add new type price : {rq}");
            try
            {
                rq.CreatedDate = DateTime.Now;
                rq.CreatedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add new type price fail:{ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Chỉnh sửa  loại gói
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPut("/typeprice/update")]
        public async Task<IActionResult> UpdateNewPost([FromBody] UpdateTypePriceCommand rq)
        {
            _logger.LogInformation($"REST request to update type price : {rq}");
            try
            {
                rq.LastModifiedDate = DateTime.Now;
                rq.LastModifiedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update type price fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// xóa gói
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpDelete("/typeprice/delete")]
        public async Task<IActionResult> DeleteTypePrice([FromBody] DeleteTypePriceCommand rq)
        {
            _logger.LogInformation($"REST request to delete type price :{rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete type price fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
