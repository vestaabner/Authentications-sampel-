using System;
using Matrix.Core;
using Matrix.Core.Interfaces;
using Matrix.Infrastructur;
using Matrix.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Matrix.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public MatrixDbContext _cmsDbContext { get; }

        public UserRepository(MatrixDbContext cmsDbContext)
        {
            _cmsDbContext = cmsDbContext;
        }

        public async Task Add(string requestEmail, byte[] passwordHash, byte[] passwordSalt)
        {
            var item = new UserEntity
            {
                Email = requestEmail,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _cmsDbContext.Users.AddAsync(item);
            await _cmsDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUser(string requestEmail)
        {
            return await _cmsDbContext.Users.Select(x => new User
            {
                Email = x.Email,
                Id = x.Id,
                Phone = x.Phone,
                PasswordHash = x.PasswordHash,
                PasswordSalt = x.PasswordSalt
            }).FirstOrDefaultAsync(x => x.Email == requestEmail);
        }
    }
}

