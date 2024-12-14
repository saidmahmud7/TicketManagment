using Dapper;
using System.Net;
using Domain.Model;
using Infrastructure.ApiResponse;
using Infrastructure.DataContext;

namespace Infrastructure.Service.TicketService;

public class TicketService(DapperContext context) : ITicketService
{
    public Response<List<Ticket>> GetAll()
    {
        var sql = "select * from tickets";
        var tickets = context.Connection().Query<Ticket>(sql).ToList();
        return new Response<List<Ticket>>(tickets);
    }

    public Response<Ticket> GetTicketById(int id)
    {
        var sql = "select * from tickets where TicketId = @id";
        var result = context.Connection().QuerySingleOrDefault<Ticket>(sql, new { id });
        return result == null
            ? new Response<Ticket>(HttpStatusCode.NotFound, "Ticket not found")
            : new Response<Ticket>(HttpStatusCode.OK, "Ticket already exists");
    }

    public Response<bool> CreateTicket(Ticket ticket)
    {
        var sql = "insert into  tickets (Type,Title,Price,EventDateTime) values (@Type,@Title,@Price,@EventDateTime)";
        var result = context.Connection().Execute(sql, ticket);
        return result == 0
            ? new Response<bool>(HttpStatusCode.NotFound, "Ticket not found")
            : new Response<bool>(HttpStatusCode.Created, "Ticket successufully added");
    }

    public Response<bool> UpdateTicket(Ticket ticket)
    {
        var sql =
            "update tickets set Type = @Type,Title=@Title,Price= @Price,EventDateTime=@EventDateTime where TicketId = @TicketId";
        var result = context.Connection().Execute(sql, ticket);
        return result > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Ticket not found")
            : new Response<bool>(HttpStatusCode.OK, "Ticket successfully updated");
    }

    public Response<bool> DeleteTicket(int id)
    {
        var ticket = GetTicketById(id).Data;
        if (ticket == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Ticket not found");
        }

        var sql = "delete from tickets where TicketId = @Id";
        var result = context.Connection().Execute(sql, new { Id = id });
        return result == 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Ticket not found")
            : new Response<bool>(HttpStatusCode.OK, "Ticket successfully deleted");
    }
}