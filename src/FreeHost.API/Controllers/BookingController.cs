using FreeHost.API.Extensions;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Requests;
using FreeHost.Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeHost.API.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    /// <summary>
    /// Fetches a list of user bookings.
    /// </summary>
    /// <returns>A list of bookings.</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/booking/bookings
    ///     GET /api/booking/bookings?page=2
    /// 
    /// </remarks>
    /// <response code="200">Returns a list of bookings</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpGet("bookings")]
    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBookings(int page = 1)
    {
        var userId = HttpContext.GetUserIdFromJwt();
        return Ok(_bookingService.GetBookings(userId, page));
    }

    /// <summary>
    /// Fetches a list of user guests.
    /// </summary>
    /// <returns>A list of guests.</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/booking/guests
    ///     GET /api/booking/guests?page=2
    /// 
    /// </remarks>
    /// <response code="200">Returns a list of guests</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpGet("guests")]
    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetGuests(int page = 1)
    {
        var userId = HttpContext.GetUserIdFromJwt();
        return Ok(_bookingService.GetGuests(userId, page));
    }

    /// <summary>
    /// Sends a booking request to the place owner
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/booking/book
    ///     {
    ///         "apartmentId": 21,
    ///         "startDate": "2022-08-10",
    ///         "endDate": "2022-08-17"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Text response</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpPost("book")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Book(BookingRequest request)
    {
        try
        {
            var userId = HttpContext.GetUserIdFromJwt();
            _bookingService.Book(request, userId);
            return Ok("Booking request has been sent to the owner.");
        }
        catch (ArgumentNullException e)
        {
            var errors = new ErrorResponse
            {
                Code = "NotFound",
                Description = e.Message
            };
            return NotFound(errors);
        }
        catch (ArgumentException e)
        {
            var errors = new ErrorResponse
            {
                Code = nameof(ArgumentException),
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

    /// <summary>
    /// Approves selected booking request and fetches a new list of user guests.
    /// </summary>
    /// <returns>A list of guests.</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/booking/approve?bookingId=13
    ///     PUT /api/booking/approve?bookingId=13&amp;page=2
    /// 
    /// </remarks>
    /// <response code="200">Returns a list of guests</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpPut("approve")]
    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Approve(int bookingId, int page = 1)
    {
        try
        {
            var userId = HttpContext.GetUserIdFromJwt();
            _bookingService.Approve(bookingId, userId);
            return Ok(_bookingService.GetGuests(userId, page));
        }
        catch (ArgumentNullException e)
        {
            var errors = new ErrorResponse
            {
                Code = "NotFound",
                Description = e.Message
            };
            return NotFound(errors);
        }
        catch (ArgumentException e)
        {
            var errors = new ErrorResponse
            {
                Code = nameof(ArgumentException),
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

    /// <summary>
    /// Rejects selected booking request and fetches a new list of user guests.
    /// </summary>
    /// <returns>A list of guests.</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/booking/reject?bookingId=13
    ///     PUT /api/booking/reject?bookingId=13&amp;page=2
    /// 
    /// </remarks>
    /// <response code="200">Returns a list of guests</response>
    /// <response code="400">Returns an array of "code", "description" error objects</response>
    [HttpPut("reject")]
    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Reject(int bookingId, int page = 1)
    {
        try
        {
            var userId = HttpContext.GetUserIdFromJwt();
            _bookingService.Reject(bookingId, userId);
            return Ok(_bookingService.GetGuests(userId, page));
        }
        catch (ArgumentNullException e)
        {
            var errors = new ErrorResponse
            {
                Code = "NotFound",
                Description = e.Message
            };
            return NotFound(errors);
        }
        catch (ArgumentException e)
        {
            var errors = new ErrorResponse
            {
                Code = nameof(ArgumentException),
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