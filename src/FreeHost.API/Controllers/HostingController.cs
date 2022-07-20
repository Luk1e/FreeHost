using FreeHost.API.Extensions;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeHost.API.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
[ApiController]
public class HostingController : ControllerBase
{
    private readonly IHostingService _hostingService;

    public HostingController(IHostingService hostingService)
    {
        _hostingService = hostingService;
    }

    [HttpGet("get/{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var userId = HttpContext.GetUserIdFromJwt();
            var place = _hostingService.GetUserPlace(id, userId);

            return Ok(place);
        }
        catch (ArgumentNullException e)
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = e.ParamName ?? string.Empty, Description = e.Message}
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

    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var userId = HttpContext.GetUserIdFromJwt();
            var places = _hostingService.GetUserPlaces(userId);

            return Ok(places);
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

    [HttpPost("add")]
    public async Task<IActionResult> Add(AddPlaceRequest request)
    {
        try
        {
            var userId = HttpContext.GetUserIdFromJwt();
            _hostingService.AddPlace(request, userId);

            return Ok();
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

    [HttpPut("edit")]
    public async Task<IActionResult> Edit(EditPlaceRequest request)
    {
        try
        {
            var userId = HttpContext.GetUserIdFromJwt();
            _hostingService.EditPlace(request, userId);

            return Ok();
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

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var userId = HttpContext.GetUserIdFromJwt();
            _hostingService.DeletePlace(id, userId);

            return Ok();
        }
        catch (ArgumentNullException e)
        {
            var errors = new List<ErrorResponse>
            {
                new() {Code = e.ParamName ?? string.Empty, Description = e.Message}
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
}