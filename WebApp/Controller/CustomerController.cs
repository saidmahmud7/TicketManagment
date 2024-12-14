using Domain.Model;
using Infrastructure.ApiResponse;
using Infrastructure.Service.CustomerService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;


[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService service):ControllerBase
{
    [HttpGet]
    public Response<List<Customer>> GetAll()
    {
        return service.GetAll();
    }

    [HttpPost]
    public Response<bool> AddCustomer(Customer customer)
    {
        return service.CreateCustomer(customer);
    }

    [HttpPut]
    public Response<bool> UpdateCustomer(Customer customer)
    {
        return service.UpdateCustomer(customer);
    }

    [HttpDelete]
    public Response<bool> DeleteCustomer(int id)
    {
        return service.DeleteCustomer(id);
    }
}