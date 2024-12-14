using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.ApiResponse;
using Infrastructure.DataContext;

namespace Infrastructure.Service.LocationService;

public class LocationService(DapperContext context) : ILocationService
{
    public Response<List<Location>> GetAll()
    {
        var sql = "select * from locations";
        var customers = context.Connection().Query<Location>(sql).ToList();
        return new Response<List<Location>>(customers);
    }

    public Response<Location> GetLocationById(int id)
    {
        var sql = "select * from locations where locationid = @id";
        var res = context.Connection().QuerySingleOrDefault<Location>(sql, new { id });
        return res == null
            ? new Response<Location>(HttpStatusCode.NotFound, "locations not found")
            : new Response<Location>(HttpStatusCode.OK, "locations already exists");
    }

    public Response<bool> CreateLocation(Location location)
    {
        var sql = "insert into  locations (city,address,locationtype) values (@City,@Address,@Locationtype)";
        var result = context.Connection().Execute(sql, location);
        return result == 0
            ? new Response<bool>(HttpStatusCode.NotFound, "locations not found")
            : new Response<bool>(HttpStatusCode.Created, "locations successufully added");
    }

    public Response<bool> UpdateLocation(Location location)
    {
        var sql =
            "update locations set  city = @city,address=@address,locationtype= @locationtype where locationid = @locationid";
        var result = context.Connection().Execute(sql, location);
        return result > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Location not found")
            : new Response<bool>(HttpStatusCode.OK, "Location successfully updated");
    }

    public Response<bool> DeleteLocation(int id)
    {
        var location = GetLocationById(id).Data;
        if (location == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Customer not found");
        }

        var sql = "delete from locations where locationid = @Id";
        var result = context.Connection().Execute(sql, new { Id = id });
        return result == 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "location not found")
            : new Response<bool>(HttpStatusCode.OK, "location successfully deleted");
    }
}