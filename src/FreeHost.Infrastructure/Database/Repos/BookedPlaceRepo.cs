using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Repositories;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Infrastructure.Database.Repos;

public class BookedPlaceRepo : Repository<BookedPlace>, IBookedPlaceRepo
{
    public BookedPlaceRepo(DbFactory dbFactory, IUnitOfWork unitOfWork, IUserRepo userRepo, IPlaceRepo placeRepo) : base(dbFactory, unitOfWork)
    {
        
    }
}