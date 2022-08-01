using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Repositories;
using FreeHost.Infrastructure.Models.Authorization;

namespace FreeHost.Infrastructure.Database.Repos;

public class UserRepo : Repository<User>, IUserRepo
{
    public UserRepo(DbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
    {

    }
}