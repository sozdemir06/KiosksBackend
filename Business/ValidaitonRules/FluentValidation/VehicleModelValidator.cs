using Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class VehicleModelValidator:AbstractValidator<VehicleModel>
    {
        public VehicleModelValidator()
        {
            RuleFor(x=>x.VehicleModelName).NotEmpty().WithMessage("Araç model adı boş olamaz...");
            RuleFor(x=>x.VehicleModelName).MaximumLength(60).WithMessage("Araç model adı en fazla 60 karakter olmalı...");
            RuleFor(x=>x.VehicleBrandId).NotEmpty().WithMessage("Araç markası boş olamaz...");
            RuleFor(x=>x.VehicleCategoryId).NotEmpty().WithMessage("Araç kategori boş olamaz...");
        }
    }
}