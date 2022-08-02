using FreeHost.Infrastructure.Models.Responses;

namespace FreeHost.Infrastructure.Interfaces.Services;

public interface IUserService
{
    UserProfileResponse GetUserProfile(string userId);
}