using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Post.Application.Commands.BoughtPostC;
using Post.Application.Queries.BoughtPostQ;
using Post.Application.Commands.SalePostC;
using Post.Application.Queries.SalePostQ;
using MediatR;
using Jhipster.Crosscutting.Constants;
using Post.Application.Commands.AdminC;
using Post.Domain.Abstractions;
using Post.Application.Queries.CommonQ;
using Post.Application.Commands.CommonC;
using Post.Application.Commands.NewPostC;
using Post.Application.Queries.DistrictQ;
using Post.Application.Commands.WardC;
using Post.Application.Queries.WardQ;
using Post.Application.Queries.NewPostQ;

namespace Post.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {

        private readonly ILogger<PostController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IPostDbContext _context;
        private readonly IDistributedCache _cache;

        public PostController(ILogger<PostController> logger, IDistributedCache cache, IConfiguration configuration, IMediator mediator, IPostDbContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _context = context;
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
            return User.FindFirst(ClaimsTypeConst.Name)?.Value;
        }

        [Authorize(Roles = RolesConstants.USER)]
        [HttpPost("/boughtpost/add")]
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

        /// <summary>
        /// Chỉnh sửa bài đăng bán
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.USER)]
        [HttpPut("/boughtpost/update")]
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

        /// <summary>
        /// Xóa bài đăng bán
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.USER)]
        [HttpDelete("/boughtpost/delete")]
        public async Task<IActionResult> DeleteBoughtPost([FromBody] DeleteBoughtPostCommand rq)
        {

            _logger.LogInformation($"REST request to delete bought post : {rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete bought post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }

        [Authorize(Roles = RolesConstants.USER)]
        [HttpPost("/boughtpost/deleteBPost")]
        public async Task<IActionResult> DeleteBoughtPost1([FromBody] DeleteBoughtPostCommand rq)
        {

            _logger.LogInformation($"REST request to delete bought post : {rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete bought post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }

        /// <summary>
        /// [Yêu cầu đăng nhập] Lấy ra những danh sách tất cả tin MUA , nếu là User thì lấy ra những tin bán của User đó, nếu là ADMIN thì lấy ra tất cả
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.USER)]
        [HttpPost("/boughtpost/search")]
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


        /// <summary>
        /// Lấy ra những tin MUA đang được hiển thị trên trang chủ mà ko cần đăng nhập
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/boughtpost/getShowing")]
        [AllowAnonymous]
        public async Task<IActionResult> GetShowingBoughtPost([FromBody] GetAllShowingBoughtPostQuery rq)
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

        /// <summary>
        /// Xem chi tiết bài đăng MUA
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Đăng tin bán - đã tích hợp trừ tiền trong ví sau khi đăng tin thành công
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.USER)]
        [HttpPost("/salepost/add")]
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


        /// <summary>
        /// Chỉnh sửa bài đăng bán
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.USER)]
        [HttpPut("/salepost/update")]
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

        /// <summary>
        /// [ADMIN] xóa bài đăng bán
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.USER)]
        [HttpDelete("/salepost/delete")]
        public async Task<IActionResult> DeleteSalePost([FromBody] DeleteSalePostCommand rq)
        {

            _logger.LogInformation($"REST request to delete sale post : {rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete sale post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }

        [Authorize(Roles = RolesConstants.USER)]
        [HttpPost("/salepost/deleteSPost")]
        public async Task<IActionResult> DeleteSalePost1([FromBody] DeleteSalePostCommand rq)
        {

            _logger.LogInformation($"REST request to delete sale post : {rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete sale post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }

        /// <summary>
        /// Lấy ra những danh sách tất cả tin BÁN , nếu là User thì lấy ra những tin bán của User đó, nếu là ADMIN thì lấy ra tất cả
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.USER)]
        [HttpPost("/salepost/search")]
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


        /// <summary>
        /// Lấy ra những tin BÁN đang được hiển thị trên trang chủ mà ko cần đăng nhập
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/salepost/getShowing")]
        [AllowAnonymous]
        public async Task<IActionResult> GetShowingSaletPost([FromBody] GetAllShowingSalePostQuery rq)
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

        /// <summary>
        /// Xem chi tiết bài đăng BÁN
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
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


        /// <summary>
        /// [ADMIN] Duyệt các bài post (tin mua - boughtPost : postType = 0, tin bán - salePost : postType = 1)
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPost("/admin/approve")]
        public async Task<IActionResult> ApprovePost([FromBody] ApprovePostCommand rq)
        {
            _logger.LogInformation($"REST request to approve post : {rq}");
            try
            {
                rq.LastModifiedDate = DateTime.UtcNow;
                rq.LastModifiedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to approve post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách khu vực để thực hiện filter - màn hình end user (type = 0 : tin MUA, type = 1 : tin BÁN)
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpGet("/post/getAllRegion")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRegionsWithCount([FromQuery] GetRegionWithCountQuery rq)
        {
            _logger.LogInformation($"REST request to get region : {rq}");
            try
            {
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get region fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách trạng thái để hiển thị ở màn quản lý tin đăng - màn hình end user (type = 0 : tin MUA, type = 1 : tin BÁN)
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.USER)]
        [HttpGet("/post/getAllStatus")]
        public async Task<IActionResult> GetStatusWithCount([FromBody] GetStatusWithCountQuery rq)
        {
            _logger.LogInformation($"REST request to get status : {rq}");
            try
            {
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get status fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Lấy ra 10 bài đăng sale theo Region
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/salepost/GetRandomSalePost")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRandomSalePost([FromBody] GetRandomSalePostQ rq)
        {
            _logger.LogInformation($"REST request to get random sale post");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get random sale post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Lấy ra 10 bài đăng bought theo Region
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/boughtpost/GetRandomBoughtPost")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRandomBoughtPost([FromBody] GetRandomBoughtPostQ rq)
        {
            _logger.LogInformation($"REST request to get random bought post");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get random bought post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Thay đổi trạng thái ở màn quản lý bài đăng (postType = 0 : Tin MUA, postType = 1 : Tin BÁN, 
        /// statusType = 0 : Hạ tin, statusType = 1 : Đẩy tin, statusType = 2 : Đăng lại - Tin mua/statusType = 2: đã bán - tin bán, status = 3: đã mua ) 
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/post/changeStatus")]
        [Authorize(Roles = RolesConstants.USER)]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusCommand rq)
        {
            _logger.LogInformation($"REST request to get change status");
            try
            {
                rq.LastModifiedDate = DateTime.UtcNow;
                rq.LastModifiedBy = GetUsernameFromContext();
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to change status fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.USER)]
        [HttpPost("/salepost/repost")]
        public async Task<IActionResult> RepostSalePost([FromBody] RepostSalePostCommand rq)
        {
            _logger.LogInformation($"REST request to repost sale post : {rq}");
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
                _logger.LogError($"REST request to repost sale post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPost("/newpost/add")]
        public async Task<IActionResult> AddNewPost([FromBody] AddNewPostCommand rq)
        {
            _logger.LogInformation($"Rest request to add new post : {rq}");
            try
            {
                rq.CreatedDate = DateTime.Now;
                rq.CreatedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add new post fail:{ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Chỉnh sửa tin tức
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPut("/newpost/update")]
        public async Task<IActionResult> UpdateNewPost([FromBody] UpdateNewPostCommand rq)
        {
            _logger.LogInformation($"REST request to update bought post : {rq}");
            try
            {
                rq.LastModifiedDate = DateTime.Now;
                rq.LastModifiedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update new post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// xóa tin tức
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpDelete("/newpost/delete")]
        public async Task<IActionResult> DeleteNewPost([FromBody] DeleteNewPostCommand rq)
        {
            _logger.LogInformation($"REST request to delete new post :{rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete new post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Xem chi tiết bài tin tức
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpGet("/newpost/id")]
        public async Task<IActionResult> ViewDetailNewPost([FromQuery] ViewDetailNewPostQuery rq)
        {
            _logger.LogInformation($"REST request to view detail new post : {rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($" REST request to view detail new post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Random new post
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/newpost/random")]
        public async Task<IActionResult> RandomNewPost([FromBody] GetRandomNewPostQuery rq)
        {
            _logger.LogInformation($"REST request to random new post : {rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($" REST request to random new post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Lấy ra những tin tức đang được hiển thị trên trang chủ mà ko cần đăng nhập
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/newpost/getShowing")]
        [AllowAnonymous]
        public async Task<IActionResult> GetShowingnewPost([FromBody] GetAllShowingNewPostQuery rq)
        {
            _logger.LogInformation($"REST request to get showing new post");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get showing new post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// [Yêu cầu đăng nhập-ADMIN] lấy ra danh sách tất cả những tin tức
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPost("/newpost/search")]
        public async Task<IActionResult> SearchNewPost([FromBody] ViewAllNewPostQuery rq)
        {
            _logger.LogInformation($"REST request to search new post");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search new post fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// [Yêu cầu đăng nhập-ADMIN] lấy ra danh sách tất cả Quận Huyện
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/district/search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchDistrict()
        {
            _logger.LogInformation($"REST request to search district");
            try
            {
                var com = new ViewAllDistrictQuery();
                var result = await _mediator.Send(com);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search district fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPost("/ward/add")]
        public async Task<IActionResult> AddWard([FromBody] AddWardCommand rq)
        {
            _logger.LogInformation($"REST request to add ward : {rq}");
            try
            {
                rq.CreatedDate = DateTime.Now;
                rq.CreatedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add ward fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Chỉnh sửa Phường,Xã
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPut("/ward/update")]
        public async Task<IActionResult> UpdateWard([FromBody] UpdateWardCommand rq)
        {
            _logger.LogInformation($"REST request to update ward : {rq}");
            try
            {
                rq.LastModifiedDate = DateTime.Now;
                rq.LastModifiedBy = GetUsernameFromContext();
                var res = await _mediator.Send(rq);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update ward fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// xóa Phường,Xã
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPost("/ward/delete")]
        public async Task<IActionResult> DeleteWard([FromBody] DeleteWardCommand rq)
        {
            _logger.LogInformation($"REST request to delete ward :{rq}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete ward fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Lấy ra danh sách tất cả những Phường,Xã
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.USER)]
        [HttpPost("/ward/search")]
        public async Task<IActionResult> SearchWard([FromBody] ViewAllWardQuery rq)
        {
            _logger.LogInformation($"REST request to search ward");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search ward fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Lấy ra danh sách những Phường,Xã theo Quận Huyện
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/ward/searchByDistrictId")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchWardByDistrictId([FromBody] ViewWardByDistrictIdQuery rq)
        {
            _logger.LogInformation($"REST request to search ward by district");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search ward by district fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Update Admin Sale
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/sale/updateAdmin")]
        public async Task<IActionResult> UpdateSalePostAdminC([FromBody] UpdateSalePostAdminC rq)
        {
            _logger.LogInformation($"REST request to update admin sale");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update admin sale fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Update Admin Bought
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [HttpPost("/bought/updateAdmin")]
        public async Task<IActionResult> UpdateBoughtPostAdminC([FromBody] UpdateBoughtPostAdminC rq)
        {
            _logger.LogInformation($"REST request to update admin bought");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update admin bought fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}

