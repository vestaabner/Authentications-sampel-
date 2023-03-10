using System;
using BackOffice.Application.Abstraction;
using BackOffice.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace BackOffice.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        public IProductRepository ProductRepository { get; }
        public IUserRepository UserRepository { get; }
    }




    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public IProductRepository ProductRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public UnitOfWork(AppDbContext context, IProductRepository productRepository, IUserRepository userRepository)
        {
            this.context = context;
            this.ProductRepository = productRepository;
            this.UserRepository = userRepository;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync() => await context.Database.BeginTransactionAsync();
        public async ValueTask DisposeAsync() { }
    }
}

