using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class FoodMenuSubScreenValidator:AbstractValidator<FoodMenuSubscreen>
    {
        public FoodMenuSubScreenValidator()
        {
            RuleFor(x => x.FoodMenuId).NotEmpty().WithMessage("İlan ID boş olamaz...");
            RuleFor(x => x.SubScreenId).NotEmpty().WithMessage("Alt Ekran ID boş olamaz...");
        }
    }
}