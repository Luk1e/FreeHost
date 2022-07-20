﻿using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Requests;
using Microsoft.AspNetCore.Identity;
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
        {
            var errors = new List<IdentityError>
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
            var error = new List<IdentityError>
            {
                new() {Code = string.Empty, Description = e.Message}
            };
            return BadRequest(error);
        }
        catch (UnauthorizedAccessException)
        {
            var errors = new List<IdentityError>
            {
                new() {Code = "IncorrectCredentials", Description = "Incorrect login or password"}
            };
            return BadRequest(errors);
        }
        catch (Exception)
        {
            var errors = new List<IdentityError>
            {
                new() {Code = "InternalServerError", Description = "Internal Server Error"}
            };
            return BadRequest(errors);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        if (string.IsNullOrEmpty(request.Login))
        {
            var errors = new List<IdentityError>
            {
                new() {Code = "InvalidLogin", Description = "Login cannot be empty"}
            };
            return BadRequest(errors);
        }
        if (string.IsNullOrEmpty(request.Password))
        {
            var errors = new List<IdentityError>
            {
                new() {Code = "InvalidPassword", Description = "Password cannot be empty"}
            };
            return BadRequest(errors);
        }
        if (string.IsNullOrEmpty(request.Email))
        {
            var errors = new List<IdentityError>
            {
                new() {Code = "InvalidEmail", Description = "Email cannot be empty"}
            };
            return BadRequest(errors);
        }
        if (string.IsNullOrEmpty(request.FirstName))
        {
            var errors = new List<IdentityError>
            {
                new() {Code = "InvalidFirstName", Description = "First name cannot be empty"}
            };
            return BadRequest(errors);
        }
        if (string.IsNullOrEmpty(request.LastName))
        {
            var errors = new List<IdentityError>
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
                .Select(ex => new IdentityError {Code = string.Empty, Description = ex.Message});
            return BadRequest(errors);
        }
        catch (UnauthorizedAccessException)
        {
            var errors = new List<IdentityError>
            {
                new() {Code = "EmailInUse", Description = "This email is already in use"}
            };
            return BadRequest(errors);
        }
        catch (Exception)
        {
            var errors = new List<IdentityError>
            {
                new() {Code = "InternalServerError", Description = "Internal Server Error"}
            };
            return BadRequest(errors);
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
            var errors = new List<IdentityError>
            {
                new() {Code = "InternalServerError", Description = "Internal Server Error"}
            };
            return BadRequest(errors);
        }
    }
}