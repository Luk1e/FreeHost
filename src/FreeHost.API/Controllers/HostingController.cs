using FreeHost.API.Extensions;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.DTOs;
using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeHost.API.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class HostingController : ControllerBase
{
    private readonly IHostingService _hostingService;

    public HostingController(IHostingService hostingService)
    {
        _hostingService = hostingService;
    }

    /// <summary>
    /// Looks for and retrieves an apartment by specified ID
    /// </summary>
    /// <param name="id">Apartment ID</param>
    /// <returns>An apartment</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/hosting/get/23
    /// 
    /// </remarks>
    /// <response code="200">Returns an apartment</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpGet("get/{id:int}")]
    [ProducesResponseType(typeof(PlaceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Retrieves apartment array for current user
    /// </summary>
    /// <returns>Apartment array</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/hosting/get
    /// 
    /// </remarks>
    /// <response code="200">Returns apartment array</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpGet("get")]
    [ProducesResponseType(typeof(IEnumerable<PlaceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Adds an apartment for current user
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/hosting/add
    ///     {
    ///         "name": "Beautiful apartment with amazing window view",
    ///         "city": "Oni",
    ///         "address": "Main st. 36, near postal office",
    ///         "numberOfBeds": 2,
    ///         "amenities": [
    ///             "Wifi",
    ///             "Iron"
    ///         ],
    ///         "photos": [
    ///             "oOJ0R1r1tk5parT7...iZJgAAAiYACACBrsAKgAA" (in base64)
    ///         ],
    ///         "distanceFromTheCenter": 350 (in meters)
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Empty response</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Edits an apartment for current user
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/hosting/edit
    ///     {
    ///         "name": "Comfortable apartment near grocery shop",
    ///         "city": "Oni",
    ///         "address": "Main st. 14",
    ///         "numberOfBeds": 3,
    ///         "amenities": [
    ///             "Iron",
    ///             "Washer"
    ///         ],
    ///         "photos": [
    ///             "oOJ0R1r1tk5parT7...iZJgAAAiYACACBrsAKgAA" (in base64)
    ///         ],
    ///         "distanceFromTheCenter": 230 (in meters)
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Empty response</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpPut("edit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Deletes an apartment for current user
    /// </summary>
    /// <param name="id">Apartment ID</param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /api/hosting/delete/2
    /// 
    /// </remarks>
    /// <response code="200">Empty response</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Retrieves an array of available amenities
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/hosting/amenities
    /// 
    /// </remarks>
    /// <response code="200">Returns an array of available amenities</response>
    [HttpGet("amenities")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAmenities()
    {
        return Ok(_hostingService.GetAmenities());
    }

    /// <summary>
    /// Retrieves an array of available cities
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/hosting/cities
    /// 
    /// </remarks>
    /// <response code="200">Returns an array of available cities</response>
    [HttpGet("cities")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCities()
    {
        return Ok(_hostingService.GetCities());
    }
}