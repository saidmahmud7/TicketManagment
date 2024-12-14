using Domain.Model;
using Infrastructure.ApiResponse;

namespace Infrastructure.Service.PurchaseService;

public interface IPurchaseService
{
    Response<List<Purchase>> GetAll();
    Response<Purchase> GetPurchaseById(int id);
    Response<bool> CreatePurchase(Purchase purchase);
    Response<bool> UpdatePurchase(Purchase purchase);
    Response<bool> DeletePurchase(int id);
}