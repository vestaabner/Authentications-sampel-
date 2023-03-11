using BackOffice.Application.Dtos;
using BackOffice.Domain.Entities;

namespace BackOffice.Application.Abstraction
{
    public interface IUserRepository : IRepository<User>
    {

        Task<bool> AddUserAsync(CreateUserDto dto);
        Task<object> Login(string mail, string pass);
        void Logout(string mail, string pass);
    }
}

