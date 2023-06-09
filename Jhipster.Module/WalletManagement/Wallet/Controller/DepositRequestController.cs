using AutoMapper;
using Jhipster.Crosscutting.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Commands.DepositRepositoryC;
using Wallet.Application.Queries.DepositRepositoryQ;
using Wallet.Application.Queries.WalletsQ;

namespace Wallet.Controller
{
    [Authorize]
    [Route("gw/[controller]")]
    [ApiController]
    public class DepositRequestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DepositRequestController> _logger;
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
        public DepositRequestController(IMediator mediator, ILogger<DepositRequestController> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Gửi yêu cầu nạp tiền tài khoản  0: Chưa xử lý , 1: Đã xác nhận , 2:Hủy bỏ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/depositRequest")]

        public async Task<IActionResult> AddDepositRequest([FromBody] AddDepositRequestC request)
        {
            _logger.LogInformation($"REST request AddDepositRequest : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.CustomerId = Guid.Parse(GetUserIdFromConext());
                request.CreatedDate = DateTime.Now;
                request.CreatedBy = GetUserIdFromConext();
                request.Status = 0;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to AddDepositRequest fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// [Admin]Cập nhật trạng thái Nạp tiền  0: Chưa xử lý , 1: Đã xác nhận , 2:Hủy bỏ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("/depositRequest")]

        public async Task<IActionResult> UpdateDepositRequest([FromBody] UpdateDepositRequestC request)
        {
            _logger.LogInformation($"REST request UpdateDepositRequest : {JsonConvert.SerializeObject(request)}");
            try
            {

                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to UpdateDepositRequest fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// [Admin] List yêu cầu nạp tiền  0: Chưa xử lý , 1: Đã xác nhận , 2:Hủy bỏ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/depositRequest/ViewAdmin")]

        public async Task<IActionResult> ViewDepositRequestByAdminQ([FromBody] ViewDepositRequestByAdminQ request)
        {
            _logger.LogInformation($"REST request ViewDepositRequestByAdmin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewDepositRequestByAdmin fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
