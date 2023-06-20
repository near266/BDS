using Jhipster.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Commands.CustomerC;
using Wallet.Application.DTO;
using Wallet.Domain.Entities;

namespace Wallet.Controller
{
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly ILogger<BannerController> _logger;

        private readonly ApplicationDatabaseContext _context;
        public BannerController(ApplicationDatabaseContext context, ILogger<BannerController> logger)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// Thêm banner
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("/banner")]
        public async Task<IActionResult> AddBanner([FromBody] BannerDTO rq)
        {
            _logger.LogInformation($"REST request to add banner : {rq}");
            try
            {
                var value = new Banner()
                {
                    Id = Guid.NewGuid(),
                    ListBanner = rq.ListBanner
                };
                await _context.banners.AddAsync(value);

                return Ok(await _context.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add banner fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Sửa banner ( truyền list ảnh mới vào )
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("/banner")]
        public async Task<IActionResult> UpdateBanner([FromBody] BannerDTO rq)
        {
            _logger.LogInformation($"REST request to update banner : {rq}");
            try
            {
                var check = await _context.banners.FirstOrDefaultAsync(i => i.Id == rq.Id);
                if (check != null)
                {
                    _context.banners.Remove(check);
                    await _context.SaveChangesAsync();
                }
                var value = new Banner()
                {
                    Id = Guid.NewGuid(),
                    ListBanner = rq.ListBanner
                };

                await _context.banners.AddAsync(value);

                return Ok(await _context.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update banner fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
