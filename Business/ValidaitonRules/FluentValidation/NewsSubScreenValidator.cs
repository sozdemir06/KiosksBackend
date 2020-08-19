using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class NewsSubScreenValidator : AbstractValidator<NewsSubScreen>
    {
        public NewsSubScreenValidator()
        {
            RuleFor(x => x.NewsId).NotEmpty().WithMessage("İlan ID boş olamaz...");
            RuleFor(x => x.SubScreenId).NotEmpty().WithMessage("Alt Ekran ID boş olamaz...");
        }
    }
}