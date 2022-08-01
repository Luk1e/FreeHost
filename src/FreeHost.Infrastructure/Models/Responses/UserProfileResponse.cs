using FreeHost.Infrastructure.Models.DTOs;

namespace FreeHost.Infrastructure.Models.Responses;

public class UserProfileResponse : UserDto
{
    public string Photo { get; set; }
    public string Email { get; set; }
}