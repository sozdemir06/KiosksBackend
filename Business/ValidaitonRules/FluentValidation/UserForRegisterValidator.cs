using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class UserForRegisterValidator:AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterValidator()
        {
            RuleFor(x=>x.FirstName).NotEmpty().WithMessage("İsim belirtiniz...");
            RuleFor(x=>x.FirstName).MaximumLength(50).WithMessage("İsim alanı en fazla 50 karakter olmalı...");
            RuleFor(x=>x.LastName).NotEmpty().WithMessage("Soyisim belirtiniz...");
            RuleFor(x=>x.LastName).MaximumLength(50).WithMessage("Soyisim alanı en fazla 50 karakter olmalı...");
            RuleFor(x=>x.Password).NotEmpty().WithMessage("Şifre belirtiniz...");
            RuleFor(x=>x.Password).Length(4,8).WithMessage("Şifreniz enaz 4 en fazla 8 karakter olmalıdı...");
            RuleFor(x=>x.GsmPhone).MaximumLength(11).WithMessage("Cep telefonu numaranız en fazla 11 karakter olmalı");
            RuleFor(x=>x.InterPhone).MaximumLength(11).WithMessage("Dahili telefonu numaranız en fazla 11 karakter olmalı");
            RuleFor(x=>x.PasswordConfirm).Equal(x=>x.Password).WithMessage("Şifre ve Şifre Tekrarı uyuşmuyor...");
            
        }
    }
}