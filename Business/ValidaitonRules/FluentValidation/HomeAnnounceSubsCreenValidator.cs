using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class HomeAnnounceSubsCreenValidator:AbstractValidator<HomeAnnounceSubScreen>
    {
        public HomeAnnounceSubsCreenValidator()
        {
            RuleFor(x=>x.HomeAnnounceId).NotEmpty().WithMessage("İlan ID boş olamaz...");
            RuleFor(x=>x.SubScreenId).NotEmpty().WithMessage("Alt Ekran ID boş olamaz...");
        }
    }
}