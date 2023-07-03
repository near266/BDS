using Jhipster.Crosscutting.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Post.Application.Queries.WardQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Commands.PriceConfigurationC;
using Wallet.Application.Commands.TermConditionConfigurationC;
using Wallet.Application.Queries.PriceConfigurationQ;
using Wallet.Application.Queries.TermConditionConfigurationQ;
using Wallet.Application.Queries.TypePriceQ;
using Wallet.Application.Queries.TypeTermQ;

namespace Wallet.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class TermConditionConfigurationController : ControllerBase
    {
        private readonly ILogger<TermConditionConfigurationController> _logger;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public TermConditionConfigurationController(ILogger<TermConditionConfigurationController> logger, IDistributedCache cache, IConfiguration configuration, IMediator mediator)
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
        #region TypeTerm
        /// <summary>
        /// lấy ra danh sách tất cả các điều khoản
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/typeterm/getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTypeTerm()
        {
            _logger.LogInformation($"REST request to get all type term");
            try
            {
                var com = new ViewAllTypeTermQuery();
                var result = await _mediator.Send(com);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all type term fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
        #region TermConditionConfiguration
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPost("/termconditionconfiguration/add")]
        public async Task<IActionResult> AddTermConditionConfiguration([FromBody] AddTermConditionConfigurationCommand rq)
        {
            _logger.LogInformation($"REST request to add term condition configuration : {rq}");
            try
            {
                rq.CreatedDate = DateTime.Now;
                rq.CreatedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add price configuration: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Chỉnh sửa  cấu hình điều khoản
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPut("/termconditionconfiguration/update")]
        public async Task<IActionResult> UpdateTermConditionConfiguration([FromBody] UpdateTermConditionConfigurationCommand rq)
        {
            _logger.LogInformation($"REST request to update term condition configuration : {rq}");
            try
            {
                rq.LastModifiedDate = DateTime.Now;
                rq.LastModifiedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update term condition configuration fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// xóa cấu hình điều khoản
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpDelete("/termconditionconfiguration/delete")]
        public async Task<IActionResult> DeleteTermConditionConfiguration([FromBody] DeleteTermConditionConfigurationCommand rq)
        {
            _logger.LogInformation($"REST request to delete term condition configuration:{rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete term condition configuration fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// lấy ra danh sách tất cả các cấu hình điều khoản
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/termconditionconfiguration/getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTermConditionConfiguration()
        {
            _logger.LogInformation($"REST request to get all type price");
            try
            {
                var com = new ViewAllTermConditionConfigurationQuery();
                var result = await _mediator.Send(com);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all type price fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("/termconditionconfiguration/ViewDetail")]
        [AllowAnonymous]
        public async Task<IActionResult> ViewDetailTermConditionConfiguration([FromQuery] ViewDetailTermConditionConfigurationQuery request)
        {
            _logger.LogInformation($"REST request to ViewDetail");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewDetail fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }
        #endregion
    }
}
