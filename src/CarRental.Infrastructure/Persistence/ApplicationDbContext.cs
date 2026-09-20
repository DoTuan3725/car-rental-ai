using CarRental.Application.Common;
using Microsoft.EntityFrameworkCore;
using CustomerEntity = CarRental.Domain.Modules.Customers.Customer;
using CarEntity = CarRental.Domain.Modules.Cars.Car;
using CarImageEntity = CarRental.Domain.Modules.Cars.CarImage;

namespace CarRental.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
    public DbSet<CarEntity> Cars => Set<CarEntity>();
    public DbSet<CarImageEntity> CarImages => Set<CarImageEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CustomerEntity>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<CustomerEntity>()
            .HasIndex(c => c.PhoneNumber)
            .IsUnique();

        modelBuilder.Entity<CarEntity>()
            .HasIndex(c => c.LicensePlate)
            .IsUnique();
    }
    // DbSet<Booking>, DbSet<Payment>, DbSet<ReturnRecord> thêm khi làm tới module đó
}