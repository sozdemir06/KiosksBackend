using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class ChangePasswordValidator : AbstractValidator<UserForChangePasswordDto>
    {
        public ChangePasswordValidator()
        {


            RuleFor(x => x.OldPassword).Length(4, 8).WithMessage("Şifreniz enaz 4 en fazla 8 karakter olmalı");
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Eski şifreniz boş olamaz...");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Şifre belirtiniz...");
            RuleFor(x => x.NewPassword).Length(4, 8).WithMessage("Yeni Şifreniz enaz 4 en fazla 8 karakter olmalı");
            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage("Yeni Şifre ve Şifre Tekrarı uyuşmuyor...");
        }
    }
}