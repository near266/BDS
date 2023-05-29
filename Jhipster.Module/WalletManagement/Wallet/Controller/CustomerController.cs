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
using Wallet.Application.Commands.CustomerC;
using Wallet.Application.Commands.WalletsC;
using Wallet.Application.Commands.WalletsPromotionaC;
using Wallet.Application.Queries.CustomerQ;

namespace Wallet.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IDistributedCache _cache;

        public CustomerController(ILogger<CustomerController> logger, IDistributedCache cache, IConfiguration configuration, IMediator mediator)
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
            return User.FindFirst(ClaimsTypeConst.Username)?.Value;
        }

        /// <summary>
        /// [ADMIN] Chỉnh sửa thông tin khách hàng
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPut("/customer/update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand rq)
        {
            _logger.LogInformation($"REST request to update customer : {rq}");
            try
            {
                rq.LastModifiedDate = DateTime.UtcNow;
                rq.LastModifiedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update customer fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// [ADMIN] Xóa khách hàng 
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpDelete("/customer/delete")]
        public async Task<IActionResult> DeleteCustomer([FromQuery] DeleteCustomerCommand rq)
        {

            _logger.LogInformation($"REST request to delete customer : {rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete customer fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết của khách hàng
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("/customer/id")]
        public async Task<IActionResult> CustomerDetail([FromQuery] ViewDetailCustomerQuery rq)
        {

            _logger.LogInformation($"REST request to view detail customer : {rq}");
            try
            {
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to view detail customer fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// [ADMIN] Xem danh sách khách hàng + số dư tài khoản
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPost("/customer/search")]
        public async Task<IActionResult> SearchCustomer([FromBody] SearchCustomerQuery rq)
        {

            _logger.LogInformation($"REST request to search customer : {rq}");
            try
            {
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search customer fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }
    }
}
