using AutoMapper;
using BackOffice.Application.Abstraction;
using BackOffice.Application.Dtos;
using BackOffice.Application.Validator;
using BackOffice.Domain.Entities;
using BackOffice.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private UserManager<User> UserManager { get; set; }
        private SignInManager<User> SignInManager { get; set; }
        private RoleManager<Roles> RoleManager { get; set; }
        public AppDbContext context;
        public IConfiguration conf { get; set; }
        public UserRepository(AppDbContext context, UserManager<User> userManager, SignInManager<User> manager, IConfiguration conf, RoleManager<Roles> roleManager) : base(context)
        {
            this.context = context;
            this.UserManager = userManager;
            this.SignInManager = manager;
            this.conf = conf;
            this.RoleManager = roleManager;
        }

        public async Task<bool> AddUserAsync(CreateUserDto dto)
        {

            UserValidator validator = new();

            var result = validator.Validate(dto);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.ToString());
                }

                return result.IsValid;
            }
            else
            {
                User newUser = new()
                {
                    Email = dto.Email,
                    UserName = dto.Email
                };

                var resNewUser = await UserManager.CreateAsync(newUser, dto.Password);

                return resNewUser.Succeeded;
            }

        }

        public async Task<object> Login(string mail, string pass)
        {
            var hasUser = await UserManager.FindByNameAsync(mail);

            if (hasUser == null)
            {
                return false;
            }
            else
            {

                var signIn = await SignInManager.PasswordSignInAsync(hasUser, pass, true, false);

                var rolesList = UserManager.GetRolesAsync(hasUser);

                var roles = await RoleManager.FindByNameAsync(rolesList.Result[0]);

                if (!signIn.Succeeded)
                {
                    return false;
                }

                var claims = new List<Claim>()
                {
                    new Claim ("Ad",hasUser.UserName.ToString()),
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role,roles.Name)
                };

                var token = GetToken(claims);

                return new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = DateTime.Now.AddDays(1)
                };

            }
        }

        public async void Logout(string mail, string pass)
        {
            await SignInManager.SignOutAsync();

        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: conf["JWT:ValidIssuer"],
                audience: conf["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext context;

        public Repository(AppDbContext dbContext)
        {
            this.context = dbContext;
        }

        public async Task<bool> AddAsync(T entity)
        {
            context.Set<T>().Add(entity);

            await context.SaveChangesAsync();

            return true;
        }


        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await context.FindAsync<T>(id);
        }

        public async Task<T?> GetByNameAsync(string name)
        {
            return await context.FindAsync<T>(name);

        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();

        }
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public AppDbContext context;

        public IMapper mapper { get; set; }
        public ProductRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            this.context = context;

            this.mapper = mapper;
        }

        public async Task<bool> AddAsync(ProductDto dto)
        {
            ProductValidator validator = new();

            var res = validator.Validate(dto);
            if (res.IsValid)
            {
                var a = mapper.Map(dto, new Product { });

                await context.Set<Product>().AddAsync(a);
                await context.SaveChangesAsync();

                return true;
            }
            else
                return false;

        }
    }
}

