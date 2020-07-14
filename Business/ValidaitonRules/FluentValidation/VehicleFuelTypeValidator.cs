using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class VehicleFuelTypeValidator:AbstractValidator<VehicleFuelTypeForCreationDto>
    {
        public VehicleFuelTypeValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Yakıt tipi adı boş olamaz..");
            RuleFor(x=>x.Name).MaximumLength(60).WithMessage("Yakıt tipi adı en fazla 60 karakter olmalı...");
        }
    }
}