using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FreeHost.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    /// <summary>
    /// Generates a pair of access token and refresh token.
    /// </summary>
    /// <returns>A pair of access and refresh tokens, first name and last name on successful authorization</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/authorization/authorize
    ///     {
    ///         "login": "myLogin",
    ///         "password": "Password_1"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Returns a pair of access and refresh tokens, first name and last name</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpPost("authorize")]
    [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Authorize([FromBody] AuthorizationRequest request)
    {
        if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = "InvalidCredentials", Description = "Invalid login or password"}
            };
            return BadRequest(errors);
        }

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
            var error = new List<ErrorResponse>
            {
                new() {Code = "UserNotFound", Description = e.Message}
            };
            return BadRequest(error);
        }
        catch (UnauthorizedAccessException e)
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = "IncorrectCredentials", Description = e.Message}
            };
            return BadRequest(errors);
        }
        catch (Exception e)
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = "InternalServerError", Description = e.Message}
            };
            return BadRequest(errors);
        }
    }

    /// <summary>
    /// Registers a user and generates a pair of access token and refresh token.
    /// </summary>
    /// <returns>A pair of access and refresh tokens, first name and last name on successful authorization</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/authorization/register
    ///     {
    ///         "email": "test@e.mail",
    ///         "password": "Password_1",
    ///         "login": "myLogin",
    ///         "firstName": "John",
    ///         "lastName": "Doe",
    ///         "photo": "oOJ0R1r1tk5parT7...iZJgAAAiYACACBrsAKgAA" (in base64)
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Returns a pair of access and refresh tokens, first name and last name</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        if (string.IsNullOrEmpty(request.Login))
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = "InvalidLogin", Description = "Login cannot be empty"}
            };
            return BadRequest(errors);
        }
        if (string.IsNullOrEmpty(request.Password))
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = "InvalidPassword", Description = "Password cannot be empty"}
            };
            return BadRequest(errors);
        }
        if (string.IsNullOrEmpty(request.Email))
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = "InvalidEmail", Description = "Email cannot be empty"}
            };
            return BadRequest(errors);
        }
        if (string.IsNullOrEmpty(request.FirstName))
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = "InvalidFirstName", Description = "First name cannot be empty"}
            };
            return BadRequest(errors);
        }
        if (string.IsNullOrEmpty(request.LastName))
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = "InvalidLastName", Description = "Last name cannot be empty"}
            };
            return BadRequest(errors);
        }

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
            var errors = passwordException.InnerExceptions
                .Select(ex => new ErrorResponse { Code = "InvalidPassword", Description = ex.Message});
            return BadRequest(errors);
        }
        catch (UnauthorizedAccessException e)
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = "IncorrectCredentials", Description = e.Message}
            };
            return BadRequest(errors);
        }
        catch (Exception e)
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = "InternalServerError", Description = e.Message}
            };
            return BadRequest(errors);
        }
    }

    /// <summary>
    /// Generates a new pair of access token and refresh token.
    /// </summary>
    /// <returns>A pair of access and refresh tokens</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/authorization/refreshtoken
    ///     {
    ///         "accessToken": "string",
    ///         "refreshToken": "string"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Returns a pair of access and refresh tokens</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpPost("refreshtoken")]
    [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var authResult = await _authorizationService.RefreshTokenAsync(request);
            if (authResult.Succeeded)
                return Ok(authResult);
            return BadRequest(authResult.Errors);
        }
        catch (Exception e)
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = "InternalServerError", Description = e.Message}
            };
            return BadRequest(errors);
        }
    }
}