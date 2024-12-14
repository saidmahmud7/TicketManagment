using Domain.Model;
using Infrastructure.ApiResponse;

namespace Infrastructure.Service.CustomerService;

public interface ICustomerService
{
    Response<List<Customer>> GetAll();
    Response<Customer> GetCustomerById(int id);
    Response<bool> CreateCustomer(Customer customer);
    Response<bool> UpdateCustomer(Customer customer);
    Response<bool> DeleteCustomer(int id);
}