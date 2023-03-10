using System;
using BackOffice.Application.Dtos;
using BackOffice.Domain.Entities;

namespace BackOffice.Application.Abstraction
{
    public interface IProductRepository : IRepository<Product>
    {

        Task<bool> AddAsync(ProductDto dto);

    }

    public interface IRepository<T> where T : class
    {

        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByNameAsync(string name);
        Task<bool> AddAsync(T entity);
        Task<List<T>> GetAllAsync();
    }
    public interface IUserRepository : IRepository<User>
    {

        Task<bool> AddUserAsync(CreateUserDto dto);
        Task<object> Login(string mail, string pass);
        void Logout(string mail, string pass);
    }
}

