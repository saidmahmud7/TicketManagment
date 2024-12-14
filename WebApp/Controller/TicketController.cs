using Domain.Model;
using Infrastructure.ApiResponse;
using Infrastructure.Service.TicketService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;




[ApiController]
[Route("api/[controller]")]
public class TicketController(ITicketService service):ControllerBase
{
    [HttpGet]
    public Response<List<Ticket>> GetAll()
    {
        return service.GetAll();
    }

    [HttpPost]
    public Response<bool> AddTicket(Ticket ticket)
    {
        return service.CreateTicket(ticket);
    }
    [HttpPut]
    public Response<bool> UpdateTicket(Ticket ticket)
    {
        return service.UpdateTicket(ticket);
    }

    [HttpDelete]
    public Response<bool> DeleteTicket(int id)
    {
        return service.DeleteTicket(id);
    }
}