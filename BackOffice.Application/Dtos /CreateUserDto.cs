using System;
namespace BackOffice.Application.Dtos
{
    public class CreateUserDto
    {

        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class LoginDto
    {

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public class ProductDto
    {

        public string? ProductName { get; set; }
        public double ProductValue { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}

