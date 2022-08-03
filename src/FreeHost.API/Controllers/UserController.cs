using FreeHost.API.Extensions;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeHost.API.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Fetches data to display on a user profile.
    /// </summary>
    /// <returns>A user profile data.</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/user
    /// 
    /// </remarks>
    /// <response code="200">Returns a user profile data</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpGet("get")]
    [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var userId = HttpContext.GetUserIdFromJwt();

            return Ok(_userService.GetUserProfile(userId));
        }
        catch (ArgumentNullException e)
        {
            var errors = new ErrorResponse
            {
                Code = "NotFound",
                Description = e.Message
            };
            return BadRequest(errors);
        }
        catch (Exception e)
        {
            var errors = new ErrorResponse
            {
                Code = "InternalServerError",
                Description = e.Message
            };
            return BadRequest(errors);
        }
    }
}