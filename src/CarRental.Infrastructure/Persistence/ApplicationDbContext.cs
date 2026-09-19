using CarRental.Application.Common;
using Microsoft.EntityFrameworkCore;
using CustomerEntity = CarRental.Domain.Modules.Customers.Customer;
using CarEntity = CarRental.Domain.Modules.Cars.Car;
using CarImageEntity = CarRental.Domain.Modules.Cars.CarImage;

namespace CarRental.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
    public DbSet<CarEntity> Cars => Set<CarEntity>();
    public DbSet<CarImageEntity> CarImages => Set<CarImageEntity>();
    // DbSet<Booking>, DbSet<Payment>, DbSet<ReturnRecord> thêm khi làm tới module đó
}