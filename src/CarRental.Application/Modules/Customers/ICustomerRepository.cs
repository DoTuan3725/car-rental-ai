using CustomerEntity = CarRental.Domain.Modules.Customers.Customer;

namespace CarRental.Application.Modules.Customers;

public interface ICustomerRepository
{
    Task<CustomerEntity?> GetByEmailAsync(string email);
    Task<CustomerEntity?> GetByIdAsync(int id);
    Task AddAsync(CustomerEntity customer);
}