using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Repositories;
using FreeHost.Infrastructure.Models.Authorization;

namespace FreeHost.Infrastructure.Database;

public class RefreshTokenRepo : Repository<RefreshToken>, IRefreshTokenRepo
{
    public RefreshTokenRepo(DbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
    {

    }
}