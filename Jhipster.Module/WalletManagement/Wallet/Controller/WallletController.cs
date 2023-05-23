﻿using AutoMapper;
using Jhipster.Crosscutting.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Commands.WalletsC;
using Wallet.Application.Commands.WalletsPromotionaC;
using Wallet.Application.Queries.WalletsPromotionalQ;
using Wallet.Application.Queries.WalletsQ;

namespace Wallet.Controller
{
    [Authorize]
    [Route("gw/[controller]")]
    [ApiController]
    public class WallletController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WallletController> _logger;
        private readonly IMapper _mapper;

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
        public WallletController(IMediator mediator, ILogger<WallletController> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        #region Wallets
        //api không cần thiết vì khi tạo tài khoản hoặc tạo khách hàng đã tạo luôn ví

        /*[HttpPost("/walletpost")]
        public async Task<ActionResult<int>> AddWallet([FromBody] AddWalletsCommand request)
        {
            _logger.LogInformation($"REST request add Wallet : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                request.CreatedBy = GetUserIdFromContext();
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Wallet fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }*/


        [HttpGet("/wallet/getByUserId")]

        public async Task<IActionResult> GetWalletByUserId([FromQuery] GetWalletByUserIdQuery request)
        {
            _logger.LogInformation($"REST request GetWalletByUserId : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.UserId = GetUserIdFromConext();
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to GetWalletByUserId fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/wallet/getall")]

        public async Task<ActionResult<int>> GetAllWallet([FromQuery] GetAllWalletQuery request)
        {
            _logger.LogInformation($"REST request GetAllWallet : {JsonConvert.SerializeObject(request)}");
            try
            {


                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Wallet fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPut("/wallet/update")]

        public async Task<ActionResult<int>> UpdateWallet([FromBody] UpdateWalletCommand request)
        {
            _logger.LogInformation($"REST request  Update Wallet : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedBy = GetUserIdFromConext();
                request.LastModifiedDate = DateTime.UtcNow;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Wallet fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("wallet/delete/{walletpost-id}")]

        public async Task<ActionResult<int>> DeleteWallet ([FromBody] DeleteWalletCommand request)
        {
            _logger.LogInformation($"REST request  Delete Wallet : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to Delete Wallet fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        // WalletsPromotional
        #region WalletPromotional
        //api không cần thiết vì khi tạo tài khoản hoặc tạo khách hàng đã tạo luôn ví

        /*[HttpPost("WalletPromotional/Add")]
        public async Task<ActionResult<int>> AddWalletPromotional([FromBody] AddWalletPromotionCommand request)
        {
            _logger.LogInformation($"REST request add Wallet : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                request.CreatedBy = GetUserIdFromContext();
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Wallet  Promotional fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }*/

        [HttpGet("WalletPromotional/Get")]

        public async Task<ActionResult<int>> GetAllWalletPromotional([FromQuery] GetAllWalletPromotionalQuery request)
        {
            _logger.LogInformation($"REST request GetAllWallet : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Wallet fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPut("WalletPromotional/Update")]

        public async Task<ActionResult<int>> UpdateWalletPromotional([FromBody] UpdateWalletPromotionCommand request)
        {
            _logger.LogInformation($"REST request  Update Wallet Promotional: {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedBy = GetUserIdFromConext();
                request.LastModifiedDate = DateTime.UtcNow;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Wallet Promotional fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("WalletPromotional/Delete/{WalletPromotion-id}")]

        public async Task<ActionResult<int>> DeleteWalletPromotional([FromBody] DeleteWalletPromotionalCommand request)
        {
            _logger.LogInformation($"REST request  Delete Wallet Promotional: {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to Delete Wallet Promotional fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

    }
}