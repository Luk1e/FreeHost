using AutoMapper;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Services;
using FreeHost.Infrastructure.Models.Responses;

namespace FreeHost.Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;

    public UserService(IUserRepo userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public UserProfileResponse GetUserProfile(string userId)
    {
        var user = _userRepo.Get(x => x.Id == userId).SingleOrDefault() ??
            throw new ArgumentNullException(nameof(userId), "User not found");

        return _mapper.Map<UserProfileResponse>(user);
    }
}