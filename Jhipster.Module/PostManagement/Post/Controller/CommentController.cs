using Jhipster.Crosscutting.Constants;
using Jhipster.Infrastructure.Migrations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Post.Application.Commands.CommentC;
using Post.Application.Commands.NotificationC;
using Post.Application.Commands.WardC;
using Post.Application.Queries.CommentQ;
using Post.Application.Queries.NotificationQ;
using Post.Application.Queries.WardQ;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Controller
{
	[ApiController]
	[Route("[controller]")]
	public class CommentController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<CommentController> _logger;
		public CommentController(IMediator mediator, ILogger<CommentController> logger)
		{
			_logger = logger;
			_mediator = mediator;
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
		[HttpPost("/comment/view")]
		[AllowAnonymous]
		public async Task<IActionResult> ViewComment([FromBody] GetAllCommentQuery rq)
		{
			_logger.LogInformation($"REST request to view comment : {rq}");
			try
			{
				if(rq.UserId != null)
				{

				rq.UserId = Guid.Parse (GetUserIdFromConext());
				}
				
				var value = await _mediator.Send(rq);
				return Ok(value);
			}
			catch (Exception ex)
			{
				_logger.LogError($"REST request to view comment fail  {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}
		[Authorize(Roles = RolesConstants.USER)]

		[HttpPost("/comment/Like")]
		public async Task<ActionResult<bool>> LikeComment([FromBody] AddUserLikeCommand rq)
		{
			_logger.LogInformation($"REST request to view comment : {rq}");
			try
			{
				bool result = false;
				rq.userId = GetUserIdFromConext();
				var value = await _mediator.Send(rq);
				var up = new UpdateCommentCommand
				{
					Id = rq.Id,
					Rely = value.rely,
					LikeCount=value.Like
				};
				await _mediator.Send(up);

				if (value?.rely?.Count() != 0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError($"REST request to view comment fail  {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}
		[Authorize(Roles = RolesConstants.USER)]
		[HttpPost("/comment/add")]
		public async Task<IActionResult> AddComment([FromBody] AddCommentCommand rq)
		{
			_logger.LogInformation($"REST request to add comment : {rq}");
			try
			{
				rq.UserId = GetUserIdFromConext();
				rq.CreatedBy = GetUsernameFromContext();
				rq.CreatedDate = DateTime.Now;	
				var res = await _mediator.Send(rq);
				return Ok(res);
			}
			catch (Exception ex)
			{
				_logger.LogError($"REST request to add comment fail: {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}
		[Authorize(Roles = RolesConstants.USER)]
		[HttpPut("/comment/update")]
		public async Task<IActionResult> UpdateComment([FromBody] UpdateContentCommand rq)
		{
			_logger.LogInformation($" REST request to update  comment  : {rq}");
			try
			{
				var value = await _mediator.Send(rq);
				//rq.LastModifiedDate = DateTime.UtcNow;
				//rq.LastModifiedBy = GetUsernameFromContext();
				//var value = await _mediator.Send(rq);
				return Ok(value);
			}
			catch (Exception ex)
			{
				_logger.LogError($"REST request to update comment fail {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}
		[Authorize(Roles = RolesConstants.USER)]
		[HttpDelete("/comment/delete")]
		public async Task<IActionResult> DeleteComment([FromBody] DeleteCommentCommand rq)
		{
			_logger.LogInformation($"REST request to delete comment :{rq}");
			try
			{
				var result = await _mediator.Send(rq);
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError($"REST request to delete comment fail: {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}
	}
}
