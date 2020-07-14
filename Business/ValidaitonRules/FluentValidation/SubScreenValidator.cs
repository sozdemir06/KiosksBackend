using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class SubScreenValidator : AbstractValidator<SubScreenForCreationDto>
    {
        public SubScreenValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Alt ekran adı boş olamaz...");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Alt ekran adı en fazla 100 karakter olmalı...");
            RuleFor(x => x.Position).NotEmpty().WithMessage("Ekran pozisyonu boş olamaz...");
            RuleFor(x => x.Position).MaximumLength(30).WithMessage("Ekran pozisyonu en fazla 30 karakter olmalı...");

        }
    }
}