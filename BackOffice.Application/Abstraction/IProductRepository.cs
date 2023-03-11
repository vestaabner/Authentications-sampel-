using System;
using BackOffice.Application.Dtos;
using BackOffice.Domain.Entities;

namespace BackOffice.Application.Abstraction
{
    public interface IProductRepository : IRepository<Product>
    {

        Task<bool> AddAsync(ProductDto dto);

    }
}

