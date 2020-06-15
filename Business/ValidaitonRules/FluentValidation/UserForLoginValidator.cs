using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class UserForLoginValidator:AbstractValidator<UserForLoginDto>
    {
        public UserForLoginValidator()
        {
            RuleFor(x=>x.Email).EmailAddress().WithMessage("Lütfen Doğru bir email adresi giriniz..");
            // RuleFor(x=>x.Email).NotEmpty().WithMessage("Email Adresi giriniz..");
            RuleFor(x=>x.Password).Length(4,8).WithMessage("Şifreniz enaz 4 en fazla 8 karakter olmalı");
            // RuleFor(x=>x.Password).NotEmpty().WithMessage("Şifre belirtiniz...");
        }
    }
}