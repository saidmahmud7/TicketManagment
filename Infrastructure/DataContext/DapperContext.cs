namespace Infrastructure.DataContext;
using Npgsql;
public class DapperContext
{
    private readonly string _context="Host=localhost;Port=5432;Database=TicketManagmentDB;User Id=postgres;Password=280806";

    public NpgsqlConnection Connection()
    {
        return new NpgsqlConnection(_context);
    }
}