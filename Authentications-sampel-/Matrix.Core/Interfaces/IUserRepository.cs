using System;

namespace Matrix.Core.Interfaces
{
    public interface IUserRepository
    {
        Task Add(string requestEmail, byte[] passwordHash, byte[] passwordSalt);
        Task<User> GetUser(string requestEmail);
    }
}

