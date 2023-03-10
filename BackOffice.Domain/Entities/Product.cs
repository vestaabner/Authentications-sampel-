using System;
using BackOffice.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace BackOffice.Domain.Entities
{
    public class Product : BaseEntity
    {

        public string ProductName { get; set; } = null!;

        public double ProductValue { get; set; }
    }

    public class Roles : IdentityRole
    {
    }
    public class User : IdentityUser
    {
        public DateTime BirthDay { get; set; }

    }
}

