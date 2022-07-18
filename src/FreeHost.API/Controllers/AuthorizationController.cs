using System.Text;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FreeHost.API.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    [HttpPost("authorize")]
    public async Task<IActionResult> Authorize([FromBody] AuthorizationRequest request)
    {
        if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
            return BadRequest("Invalid login or password");

        try
        {
            var authResult = await _authorizationService.AuthorizeAsync(request);
            if (!authResult.Succeeded)
            {
                return BadRequest(authResult.Errors);
            }
            
            return Ok(authResult);
        }
        catch (NullReferenceException e)
        {
            return BadRequest(e.Message);
        }
        catch (UnauthorizedAccessException)
        {
            return BadRequest("Incorrect login or password");
        }
        catch (Exception)
        {
            return BadRequest("InternalServerError");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        if (string.IsNullOrEmpty(request.Login))
            return BadRequest("Login cannot be empty");
        
        if (string.IsNullOrEmpty(request.Password))
            return BadRequest("Password cannot be empty");
        
        if (string.IsNullOrEmpty(request.Email))
            return BadRequest("Email cannot be empty");
        
        if (string.IsNullOrEmpty(request.FirstName))
            return BadRequest("First name cannot be empty");

        if (string.IsNullOrEmpty(request.LastName))
            return BadRequest("Last name cannot be empty");

        try
        {
            var result = await _authorizationService.RegisterAsync(request);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            
            return Ok(result);

        }
        catch (AggregateException passwordException)
        {
            var sb = new StringBuilder();
            foreach (var ex in passwordException.InnerExceptions)
            {
                sb.AppendLine(ex.Message);
            }
            return BadRequest(sb.ToString());
        }
        catch (UnauthorizedAccessException)
        {
            return BadRequest("This email is already in use");
        }
        catch (Exception)
        {
            return BadRequest("InternalServerError");
        }
    }

    [HttpPost("refreshtoken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var authResult = await _authorizationService.RefreshTokenAsync(request);
            if (authResult.Succeeded)
                return Ok(authResult);
            return BadRequest(authResult.Errors);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}