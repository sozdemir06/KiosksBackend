using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class CurrencyValidator:AbstractValidator<Currency>
    {
        public CurrencyValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Paranın adı boş olamaz...");
            RuleFor(x=>x.ShorName).NotEmpty().WithMessage("Paranın kısa adı boş olamaz..");
            RuleFor(x=>x.Symbol).NotEmpty().WithMessage("Paranın sembolü boş olamaz...");
        }
    }
}