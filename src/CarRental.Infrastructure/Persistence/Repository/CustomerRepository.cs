using CarRental.Application.Modules.Customers;
using Microsoft.EntityFrameworkCore;
using CustomerEntity = CarRental.Domain.Modules.Customers.Customer;

namespace CarRental.Infrastructure.Persistance.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    public CustomerRepository(ApplicationDbContext context) => _context = context;

    public Task<CustomerEntity?> GetByEmailAsync(string email)
        => _context.Customers.FirstOrDefaultAsync(c => c.Email == email);

    public async Task<CustomerEntity?> GetByIdAsync(int id)
        => await _context.Customers.FindAsync(id);

    public async Task AddAsync(CustomerEntity customer)
        => await _context.Customers.AddAsync(customer);
}