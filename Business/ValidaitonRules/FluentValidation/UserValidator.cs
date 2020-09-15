using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x=>x.Email).EmailAddress().WithMessage("Lütfen Doğru bir email adresi giriniz..");
            RuleFor(x=>x.Email).NotEmpty().WithMessage("Email Adresi giriniz..");
            RuleFor(x=>x.FirstName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz..");
            RuleFor(x=>x.LastName).NotEmpty().WithMessage("Kullanıcı soyadı boş olamaz..");
            RuleFor(x=>x.InterPhone).NotEmpty().WithMessage("Dahili telefon boş olamaz..");
            RuleFor(x=>x.CampusId).NotEmpty().WithMessage("Yerleşke seçiniz..");
            RuleFor(x=>x.DepartmentId).NotEmpty().WithMessage("Birim seçiniz..");
            RuleFor(x=>x.Degree).NotEmpty().WithMessage("Ünvan seçiniz..");
        }
    }
}