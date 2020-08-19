using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class FoodMenuPhotoValidator : AbstractValidator<FoodMenuPhoto>
    {
        public FoodMenuPhotoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Foroğraf'ın adı boş olamaz...");
            RuleFor(x => x.FullPath).NotEmpty().WithMessage("Foroğraf'ın tam adresi boş olamaz...");
            RuleFor(x => x.FoodMenuId).NotEmpty().WithMessage("Fotoğraf'ın hangi ilana ait olduğunu belirtiniz...");
        }
    }
}