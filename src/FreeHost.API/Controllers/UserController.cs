using FreeHost.API.Extensions;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FreeHost.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("get")]
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