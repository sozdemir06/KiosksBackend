using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class VehicleEngineSizeValidator : AbstractValidator<VehicleEngineSizeForCreationDto>
    {
        public VehicleEngineSizeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Opsiyon adı boş olamaz..");
            RuleFor(x => x.Name).MaximumLength(60).WithMessage("Opsiyon adı en fazla 60 karakter olmalı...");
        }
    }
}