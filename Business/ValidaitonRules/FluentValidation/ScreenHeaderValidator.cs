using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class ScreenHeaderValidator:AbstractValidator<ScreenHeaderForCreationDto>
    {
        public ScreenHeaderValidator()
        {
            RuleFor(x=>x.HeaderText).MaximumLength(70).WithMessage("Başlık Yazısı en fazla 70 karakter olmalı");
            RuleFor(x=>x.ScreenId).NotEmpty().WithMessage("Ekran seçimi yapmadınız");
        }
    }
}