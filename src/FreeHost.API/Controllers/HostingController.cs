using FreeHost.API.Extensions;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Requests;
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
            return BadRequest(e.Message);
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
            return BadRequest(e.Message);
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
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var userId = HttpContext.GetUserIdFromJwt();
            _hostingService.DeletePlace(id, userId);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}