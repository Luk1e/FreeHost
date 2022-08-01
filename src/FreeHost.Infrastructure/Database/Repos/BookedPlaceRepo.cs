using System.Linq.Expressions;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Repositories;
using FreeHost.Infrastructure.Models.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FreeHost.Infrastructure.Database.Repos;

public class BookedPlaceRepo : Repository<BookedPlace>, IBookedPlaceRepo
{
    public BookedPlaceRepo(DbFactory dbFactory, IUnitOfWork unitOfWork, IUserRepo userRepo, IPlaceRepo placeRepo) : base(dbFactory, unitOfWork)
    {
        
    }

    public override IQueryable<BookedPlace> Get(Expression<Func<BookedPlace, bool>> expression)
    {
        return base.Get(expression)
            .Include(x => x.Owner)
            .Include(x => x.Place).ThenInclude(x => x.City)
            .Include(x => x.Client);
    }
}