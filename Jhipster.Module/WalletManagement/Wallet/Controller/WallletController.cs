using AutoMapper;
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
using Wallet.Application.Queries.HistoryQ;
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
        
        /// <summary>
        /// Lấy số dư tài khoản theo đăng nhập
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("/wallet/getByUserId")]

        public async Task<IActionResult> GetWalletByUserId([FromQuery] GetWalletByUserIdQuery request)
        {
            _logger.LogInformation($"REST request GetWalletByUserId : {JsonConvert.SerializeObject(request)}");
            try
            {
               
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to GetWalletByUserId fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// [ADMIN] Nạp tiền vào ví chính
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
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

        #endregion

        // WalletsPromotional
        #region WalletPromotional

        /// <summary>
        /// [ADMIN] Nạp tiền vào ví khuyến mại
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
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

        #endregion
        /// <summary>
        /// [ADMIN] Xem biến động số dư (type : 0-Nạp Tiền, 1-Trừ tiền, 2-Hoàn tiền / walletType : 0-ví chính, 1:ví khuyến mại)
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.USER)]
        [HttpPost("wallet/searchTransaction")]
        public async Task<ActionResult<int>> SearchTransaction([FromBody] SearchTransactionQuery request)
        { 
            _logger.LogInformation($"REST request Update search transaction: {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update search transaction fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
