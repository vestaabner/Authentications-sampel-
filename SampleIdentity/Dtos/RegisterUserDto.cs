using System.ComponentModel.DataAnnotations;

namespace SampleIdentity.Dtos
{
    public class RegisterUserDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }


        [Required]
        public string LastName { get; set; }

        
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        
        
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(PassWord))]
        public string ConfirmPassWord { get; set; }

    }
    public class LoginDto
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
