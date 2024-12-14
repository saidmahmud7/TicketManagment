using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.ApiResponse;
using Infrastructure.DataContext;

namespace Infrastructure.Service.PurchaseService;

public class PurchaseService(DapperContext context) : IPurchaseService
{
    public Response<List<Purchase>> GetAll()
    {
        var sql = "select * from purchases";
        var purchase = context.Connection().Query<Purchase>(sql).ToList();
        return new Response<List<Purchase>>(purchase);
    }

    public Response<Purchase> GetPurchaseById(int id)
    {
        var sql = "select * from purchases where purchasesid = @id";
        var res = context.Connection().QuerySingleOrDefault<Purchase>(sql, new { id });
        return res == null
            ? new Response<Purchase>(HttpStatusCode.NotFound, "purchase not found")
            : new Response<Purchase>(HttpStatusCode.OK, "purchase already exists");
    }

    public Response<bool> CreatePurchase(Purchase purchase)
    {
        var sql = "insert into  purchases (ticketid,customerid,PurchaseDateTime,Quantity,TotalPrice) values (@ticketid,@customerid,@PurchaseDateTime,@Quantity,@TotalPrice)";
        var result = context.Connection().Execute(sql, purchase);
        return result == 0
            ? new Response<bool>(HttpStatusCode.NotFound, "Purchase not found")
            : new Response<bool>(HttpStatusCode.Created, "Purchase successufully added");    }

    public Response<bool> UpdatePurchase(Purchase purchase)
    {
        var sql =
            "update purchases set  ticketid = @ticketid,customerid=@customerid,PurchaseDateTime= @PurchaseDateTime,Quantity=@Quantity,TotalPrice=@TotalPrice where PurchaseId = @PurchaseId";
        var result = context.Connection().Execute(sql, purchase);
        return result > 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Purchase not found")
            : new Response<bool>(HttpStatusCode.OK, "Purchase successfully updated");    }

    public Response<bool> DeletePurchase(int id)
    {
        var purchase = GetPurchaseById(id).Data;
        if (purchase == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Customer not found");
        }

        var sql = "delete from purchases where PurchaseId = @Id";
        var result = context.Connection().Execute(sql, new { Id = id });
        return result == 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Purchase not found")
            : new Response<bool>(HttpStatusCode.OK, "Purchase successfully deleted");    }
}