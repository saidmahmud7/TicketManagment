using Domain.Model;
using Infrastructure.ApiResponse;

namespace Infrastructure.Service.LocationService;

public interface ILocationService
{
    Response<List<Location>> GetAll();
    Response<Location> GetLocationById(int id);
    Response<bool> CreateLocation(Location location);
    Response<bool> UpdateLocation(Location location);
    Response<bool> DeleteLocation(int id);
}