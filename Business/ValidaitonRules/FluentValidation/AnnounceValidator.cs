using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class AnnounceValidator:AbstractValidator<Announce>
    {
        public AnnounceValidator()
        {
            RuleFor(x=>x.Header).NotEmpty().WithMessage("İlan başlığı boş olamaz...");
            RuleFor(x=>x.Header).MaximumLength(140).WithMessage("İlan başlığı en fazla 140 karakter olmalı...");
            //RuleFor(x=>x.Content).NotEmpty().WithMessage("İlan İçeriği en fazla 500 karakter olmalı");
            RuleFor(x=>x.UserId).NotEmpty().WithMessage("ilan için bir kullanıcı seçiniz...");
            
        }
    }
}