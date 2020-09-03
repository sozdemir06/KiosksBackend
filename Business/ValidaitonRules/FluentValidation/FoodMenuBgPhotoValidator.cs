using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class FoodMenuBgPhotoValidator : AbstractValidator<FoodMenuBgPhoto>
    {
        public FoodMenuBgPhotoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Foroğraf'ın adı boş olamaz...");
            RuleFor(x => x.FullPath).NotEmpty().WithMessage("Foroğraf'ın tam adresi boş olamaz...");
        }
    }
}