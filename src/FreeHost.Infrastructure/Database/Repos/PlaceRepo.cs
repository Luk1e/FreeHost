using System.Linq.Expressions;
using FreeHost.Infrastructure.Interfaces.Database;
using FreeHost.Infrastructure.Interfaces.Repositories;
using FreeHost.Infrastructure.Models.Authorization;
using FreeHost.Infrastructure.Models.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FreeHost.Infrastructure.Database.Repos;

public class PlaceRepo : Repository<Place>, IPlaceRepo
{
    private readonly ICityRepo _cityRepo;
    private readonly IAmenityRepo _amenityRepo;
    private readonly IUserRepo _userRepo;
    private readonly IAmenityPlaceRepo _amenityPlaceRepo;
    private readonly IPhotoRepo _photoRepo;

    public PlaceRepo(DbFactory dbFactory, IUnitOfWork unitOfWork, ICityRepo cityRepo, IAmenityRepo amenityRepo, IUserRepo userRepo, IAmenityPlaceRepo amenityPlaceRepo, IPhotoRepo photoRepo) : base(dbFactory, unitOfWork)
    {
        _cityRepo = cityRepo;
        _amenityRepo = amenityRepo;
        _userRepo = userRepo;
        _amenityPlaceRepo = amenityPlaceRepo;
        _photoRepo = photoRepo;
    }

    public void Add(Place entity, string userId)
    {
        entity = PopulatePlace(entity, userId);
        Add(entity);
    }

    public void Update(Place entity, string userId)
    {
        if (entity.Id != default)
        {
            _amenityPlaceRepo.DeleteRange(_amenityPlaceRepo.Get(x => x.PlacesId == entity.Id));
            _photoRepo.DeleteRange(_photoRepo.Get(x => x.PlaceId == entity.Id));
        }

        entity = PopulatePlace(entity, userId);
        Update(entity);
    }

    public void Delete(int placeId, string userId)
    {
        var place = Get(x => x.Id == placeId && x.User.Id == userId).SingleOrDefault() ??
                    throw new ArgumentNullException(nameof(Place), "Place not found");
        Delete(place);
    }

    public override IQueryable<Place> Get(Expression<Func<Place, bool>> expression)
    {
        return base.Get(expression)
            .Include(c => c.City)
            .Include(ad => ad.BookedDates)
            .Include(a => a.Amenities)
            .Include(p => p.Photos)
            .Include(u => u.User);
    }

    private Place PopulatePlace(Place entity, string userId)
    {
        var city = _cityRepo.Get(x => x.Name == entity.City.Name).SingleOrDefault() ??
                   throw new ArgumentNullException(nameof(City), $"City '{entity.City.Name}' not found");
        entity.City = city;

        var amenities = new List<Amenity>(entity.Amenities.Count());
        amenities.AddRange(entity.Amenities.Select(amenitySrc =>
            _amenityRepo.Get(x => x.Name == amenitySrc.Name).SingleOrDefault() ??
            throw new ArgumentNullException(nameof(Amenity), $"Amenity '{amenitySrc.Name}' not found")));
        entity.Amenities = amenities;

        entity.User = _userRepo.Get(x => x.Id == userId).SingleOrDefault() ??
                      throw new ArgumentNullException(nameof(User), "User not found");

        return entity;
    }
}