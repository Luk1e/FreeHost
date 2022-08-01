using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Repositories;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Infrastructure.Database.Repos;

public class AmenityPlaceRepo : Repository<AmenityPlace>, IAmenityPlaceRepo
{
    public AmenityPlaceRepo(DbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
    {

    }
}