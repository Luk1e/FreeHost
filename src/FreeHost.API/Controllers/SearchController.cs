using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeHost.API.Controllers;

//[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    /// <summary>
    /// Fetches a list of apartments by City and date range.
    /// </summary>
    /// <returns>A list of apartments.</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/search?city=Oni&amp;startDate=2022-07-19&amp;endDate=2022-07-25
    ///     GET /api/search?city=Oni&amp;startDate=2022-07-19&amp;endDate=2022-07-25&amp;numberOfBeds=2&amp;sortBy=1&amp;page=2
    /// 
    /// </remarks>
    /// <response code="200">Returns a list of apartments</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpGet]
    [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] SearchRequest request)
    {
        if (request.EndDate < request.StartDate)
        {
            var errors = new ErrorResponse
            {
                Code = "InvalidDate",
                Description = "Check-out date cannot be earlier than check-in date"
            };
            return BadRequest(errors);
        }

        try
        {
            var result = _searchService.FetchPlaces(request);
        
            return Ok(result);
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