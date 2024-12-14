using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.ApiResponse;
using Infrastructure.DataContext;

namespace Infrastructure.Service.CustomerService;

public class CustomerService(DapperContext context) : ICustomerService
{
    public Response<List<Customer>> GetAll()
    {
        var sql = "select * from customers";
        var customers = context.Connection().Query<Customer>(sql).ToList();
        return new Response<List<Customer>>(customers);
    }

    public Response<Customer> GetCustomerById(int id)
    {
        var sql = "select * from customers where CustomerId = @id";
        var res = context.Connection().QuerySingleOrDefault<Customer>(sql, new { id });
        return res == null
            ? new Response<Customer>(HttpStatusCode.NotFound, "Customer not found")
            : new Response<Customer>(HttpStatusCode.OK, "Customer already exists");
    }

    public Response<bool> CreateCustomer(Customer customer)
    {
        var sql = "insert into  customers (FullName,Email,Phone) values (@FullName,@Email,@Phone)";
        var result = context.Connection().Execute(sql, customer);
        return result == 0
            ? new Response<bool>(HttpStatusCode.NotFound, "Customer not found")
            : new Response<bool>(HttpStatusCode.Created, "Customer successufully added");
    }

    public Response<bool> UpdateCustomer(Customer customer)
    {
        var sql =
            "update customers set FullName = @FullName,Email=@Email,Phone= @Phone where CustomerId = @CustomerId";
        var result = context.Connection().Execute(sql, customer);
        return result > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Customer not found")
            : new Response<bool>(HttpStatusCode.OK, "Customer successfully updated");
    }

    public Response<bool> DeleteCustomer(int id)
    {
        var customer = GetCustomerById(id).Data;
        if (customer == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Customer not found");
        }

        var sql = "delete from customers where CustomerId = @Id";
        var result = context.Connection().Execute(sql, new { Id = id });
        return result == 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Customer not found")
            : new Response<bool>(HttpStatusCode.OK, "Customer successfully deleted");
    }
}