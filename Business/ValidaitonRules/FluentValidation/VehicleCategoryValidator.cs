using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class VehicleCategoryValidator : AbstractValidator<VehicleCategoryForCreationDto>
    {
        public VehicleCategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Araç Kategori adı boş olamaz...");
            RuleFor(x => x.CategoryName).MaximumLength(60).WithMessage("Araç Kategori adı en fazla 60 karakter olmalı...");
        }
    }
}