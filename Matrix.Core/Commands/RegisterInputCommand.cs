using System;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Matrix.Core.Commands
{
    public class RegisterInputVm
    {
        [Required(ErrorMessage = "ایمیل لازم است.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "کلمه عبور لازم است.")]
        public string Password { get; set; }
    }


    public class RegisterInputCommand : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}

