using Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class VehicleBrandValidator:AbstractValidator<VehicleBrand>
    {
        public VehicleBrandValidator()
        {
            RuleFor(x=>x.BrandName).NotEmpty().WithMessage("Marka adı boş olamaz...");
            RuleFor(x=>x.BrandName).MaximumLength(60).WithMessage("Marka adı en fazla 60 karakter olmalı...");
            RuleFor(x=>x.VehicleCategoryId).NotEmpty().WithMessage("Marka için kategori seçiniz...");
        }
    }
}