using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class VehicleGearTypeValidator:AbstractValidator<VehicleGearTypeForCreationDto>
    {
        public VehicleGearTypeValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Vites tipi adı boş olamaz...");
            RuleFor(x=>x.Name).MaximumLength(60).WithMessage("Vites tipi adı en fazla 60 karakter olmalı...");
        }
    }
}