using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class CityValidator:AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Şehir adı boş olamaz..");
            
        }
    }
}