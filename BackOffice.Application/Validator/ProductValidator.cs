using System;
using BackOffice.Application.Dtos;
using FluentValidation;

namespace BackOffice.Application.Validator
{
    //public class ProductValidator : AbstractValidator<ProductDto>
    //{

    //    public ProductValidator()
    //    {
    //        RuleFor(X => X.ProductValue).NotEmpty().Must(p => p > 100).WithMessage("Değer kabul edilenden düşük.");
    //        RuleFor(x => x.ProductName).NotEmpty().MaximumLength(100).WithMessage("İsim çok uzun!");
    //    }

    //}
    //public class UserValidator : AbstractValidator<CreateUserDto>
    //{

    //    public UserValidator()
    //    {
    //        //RuleFor(u=> u.FullName).NotEmpty().WithMessage("Boş bırakılamaz.");
    //        //RuleFor(u => u.PasswordHash).NotEmpty().WithMessage("Boş bırakılamaz.");
    //        //RuleFor(u => u.UserName).NotEmpty().WithMessage("Boş bırakılamaz.");
    //        RuleFor(u => u.Email).NotEmpty().WithMessage("Boş bırakılamaz.");
    //        RuleFor(u => u.Password).NotEmpty().WithMessage("Boş bırakılamaz.");
    //    }

    //}
}

