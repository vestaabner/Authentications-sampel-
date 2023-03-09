using System;
using Matrix.Core.Commands;
using Matrix.Core.Interfaces;
using MediatR;

namespace Matrix.Core.Handlers
{
    public  class RegisterHandler : IRequestHandler<RegisterInputCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public RegisterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(RegisterInputCommand request, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            var existUser = await _userRepository.GetUser(request.Email);

            if (existUser != null)
            {
                throw new Exception("User Already Exist");
            }
            await _userRepository.Add(request.Email, passwordHash, passwordSalt);

            return "";
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}

