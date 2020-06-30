using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class BuildingAgeValidator:AbstractValidator<BuildingAgeForCretationDto>
    {
        public BuildingAgeValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Opsiyon adı boş olamaz...");
            RuleFor(x=>x.Name).MaximumLength(60).WithMessage("Opsiyon adı en fazla 60 karakter olmalı...");
        }
    }
}