using Domain.Model;
using Infrastructure.ApiResponse;
using Infrastructure.Service.PurchaseService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;




[ApiController]
[Route("api/[controller]")]
public class PurchaseController(IPurchaseService service) :ControllerBase
{
    [HttpGet]
    public Response<List<Purchase>> GetAll()
    {
        return service.GetAll();
    }

    [HttpPost]
    public Response<bool> AddPurchase(Purchase purchase)
    {
        return service.CreatePurchase(purchase);
    }

    [HttpPut]
    public Response<bool> UpdatePurchase(Purchase location)
    {
        return service.UpdatePurchase(location);
    }

    [HttpDelete]
    public Response<bool> DeletePurchase(int id)
    {
        return service.DeletePurchase(id);
    }
}