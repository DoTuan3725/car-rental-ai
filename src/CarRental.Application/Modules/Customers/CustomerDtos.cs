namespace CarRental.Application.Modules.Customers;

public record RegisterRequest(string FullName, string Email, string PhoneNumber, string Password);
public record LoginRequest(string Email, string Password);
public record AuthResponse(string Token, string FullName, string Role);
public record CustomerDto(int Id, string FullName, string Email, string PhoneNumber, string Role);