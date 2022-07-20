using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Repositories;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Infrastructure.Database;

public class PhotoRepo : Repository<Photo>, IPhotoRepo
{
    public PhotoRepo(DbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
    {

    }
}