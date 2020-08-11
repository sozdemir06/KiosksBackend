using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class AnnouncesubScreenValidator : AbstractValidator<AnnounceSubScreen>
    {
        public AnnouncesubScreenValidator()
        {
            RuleFor(x => x.AnnounceId).NotEmpty().WithMessage("İlan ID boş olamaz...");
            RuleFor(x => x.SubScreenId).NotEmpty().WithMessage("Alt Ekran ID boş olamaz...");
        }
    }
}