using Domain.Model;
using Infrastructure.ApiResponse;

namespace Infrastructure.Service.TicketService;

public interface ITicketService
{
    Response<List<Ticket>> GetAll();
    Response<Ticket> GetTicketById(int id);
    Response<bool> CreateTicket(Ticket ticket);
    Response<bool> UpdateTicket(Ticket ticket);
    Response<bool> DeleteTicket(int id);
}