using CarRental.Application.Common;
using CarRental.Domain.Common;
using CarRental.Application.Modules.Customers;
using CustomerEntity = CarRental.Domain.Modules.Customers.Customer;

namespace CarRental.Application.Modules.Customers;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(
        ICustomerRepository customerRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (await _customerRepository.GetByEmailAsync(request.Email) is not null)
            throw new ConflictException("Email đã được sử dụng.");

        var customer = new CustomerEntity
        {
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = _passwordHasher.Hash(request.Password),
            Role = UserRole.Customer
        };

        await _customerRepository.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(customer);
        return new AuthResponse(token, customer.FullName, customer.Role.ToString());
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var customer = await _customerRepository.GetByEmailAsync(request.Email)
            ?? throw new UnauthorizedAccessException("Email hoặc mật khẩu không đúng.");

        if (!_passwordHasher.Verify(request.Password, customer.PasswordHash))
            throw new UnauthorizedAccessException("Email hoặc mật khẩu không đúng.");

        var token = _jwtTokenGenerator.GenerateToken(customer);
        return new AuthResponse(token, customer.FullName, customer.Role.ToString());
    }

    public async Task<CustomerDto?> GetByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return customer is null
            ? null
            : new CustomerDto(customer.Id, customer.FullName, customer.Email, customer.PhoneNumber, customer.Role.ToString());
    }
}