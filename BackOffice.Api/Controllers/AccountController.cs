using System;
using BackOffice.Application.Dtos;
using BackOffice.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Api.Controllers
{
    [ApiController]
    [Route("/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {

            return Ok(await unitOfWork.UserRepository.AddUserAsync(dto));

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            return Ok(await unitOfWork.UserRepository.Login(dto.Email, dto.Password));
        }
    }
}

