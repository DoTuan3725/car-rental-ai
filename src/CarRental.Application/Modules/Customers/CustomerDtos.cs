using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Modules.Customers;

public record RegisterRequest(
    [property: Required] string FullName,
    [property: Required, EmailAddress] string Email,
    [property: Required, Phone] string PhoneNumber,
    [property: Required, MinLength(6)] string Password);
public record LoginRequest(string Email, string Password);
public record AuthResponse(string Token, string FullName, string Role);
public record CustomerDto(int Id, string FullName, string Email, string PhoneNumber, string Role, string? IdentityNumber, string? Address);

public record UpdateProfileRequest(string FullName, string? IdentityNumber, string? Address);