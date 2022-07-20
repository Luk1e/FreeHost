using FreeHost.Infrastructure.Interfaces.Repositories;
using FreeHost.Infrastructure.Models.Hosting;

namespace FreeHost.Infrastructure.Interfaces.Database;

public interface IPlaceRepo : IRepository<Place>
{
    /// <summary>
    /// Do NOT use base Add() method. Use this method instead, to Add a Place with User attached to it.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    void Add(Place entity, string userId);

    /// <summary>
    /// Do NOT use base Update() method. Use this method instead, to Update a Place with User attached to it.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    void Update(Place entity, string userId);

    /// <summary>
    /// Do NOT use base Delete() method. Use this method instead, to Delete a Place that belongs to the User invoking this action.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    void Delete(int placeId, string userId);
}