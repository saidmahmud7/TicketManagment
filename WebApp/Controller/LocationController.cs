using Domain.Model;
using Infrastructure.ApiResponse;
using Infrastructure.Service.LocationService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;
[ApiController]
[Route("api/[controller]")]
public class LocationController(ILocationService service) :ControllerBase
{
    [HttpGet]
    public Response<List<Location>> GetAll()
    {
        return service.GetAll();
    }

    [HttpPost]
    public Response<bool> AddLocation(Location location)
    {
        return service.CreateLocation(location);
    }

    [HttpPut]
    public Response<bool> UpdateCustomer(Location location)
    {
        return service.UpdateLocation(location);
    }

    [HttpDelete]
    public Response<bool> DeleteCustomer(int id)
    {
        return service.DeleteLocation(id);
    }
}