using System;
using System.ComponentModel.DataAnnotations;

namespace Matrix.Core
{
	public class User
	{
    
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Phone { get; set; }
    }
}

