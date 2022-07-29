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
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost("book")]
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

    [HttpPut("approve")]
    public async Task<IActionResult> Approve(int bookingId)
    {
        try
        {
            var userId = HttpContext.GetUserIdFromJwt();
            _bookingService.Approve(bookingId, userId);
            return Ok("You have approved this booking");
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