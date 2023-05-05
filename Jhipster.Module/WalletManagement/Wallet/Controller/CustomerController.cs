﻿using Jhipster.Crosscutting.Constants;
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

        /*[HttpPost("/customer/add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerCommand rq)
        {
            _logger.LogInformation($"REST request to add customer : {rq}");
            try
            {
                rq.Id = Guid.NewGuid();
                rq.CreatedDate = DateTime.UtcNow;
                rq.CreatedBy = GetUsernameFromContext();

                var wallet = new AddWalletsCommand
                {
                    Id = Guid.NewGuid(),
                    Username = "string",
                    Amount = 0,
                    Currency = "Đồng",
                    CustomerId = rq.Id,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = GetUsernameFromContext(),
                };

                var walletPro = new AddWalletPromotionCommand
                {
                    Id = Guid.NewGuid(),
                    Username = "string",
                    Amount = 0,
                    Currency = "Đồng",
                    CustomerId = rq.Id,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = GetUsernameFromContext(),
                };

                var res = await _mediator.Send(rq);
                var res1 = await _mediator.Send(wallet);
                var res2 = await _mediator.Send(walletPro);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add customer fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }*/

        [HttpPut("/customer/update")]
        [AllowAnonymous]
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

        [HttpDelete("/customer/delete")]
        [AllowAnonymous]
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
        [HttpGet("/customer/id")]
        [AllowAnonymous]
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

        [HttpPost("/customer/search")]
        [AllowAnonymous]
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
