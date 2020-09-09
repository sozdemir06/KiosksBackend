using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class ScreenFooterValidator:AbstractValidator<ScreenFooterForCreationDto>
    {
        public ScreenFooterValidator()
        {
            RuleFor(x=>x.ScreenId).NotEmpty().WithMessage("Ekran seçimi yapmadınız...");
            RuleFor(x=>x.FooterText).NotEmpty().WithMessage("Footer metni boş olamaz...");
        }
    }
}