using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class HeatingTypeValidator : AbstractValidator<HeatingTypeForCreationDto>
    {
        public HeatingTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Opsiyon adı boş olamaz...");
            RuleFor(x => x.Name).MaximumLength(60).WithMessage("Opsiyon adı en fazla 60 karakter olmalı...");
        }
    }
}