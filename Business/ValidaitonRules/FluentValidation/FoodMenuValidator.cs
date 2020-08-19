using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class FoodMenuValidator : AbstractValidator<FoodMenu>
    {
        public FoodMenuValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("ilan için bir kullanıcı seçiniz...");
        }
    }
}