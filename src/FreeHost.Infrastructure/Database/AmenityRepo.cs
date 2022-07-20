using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Repositories;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Infrastructure.Database;

public class AmenityRepo : Repository<Amenity>, IAmenityRepo
{
    public AmenityRepo(DbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
    {

    }
}