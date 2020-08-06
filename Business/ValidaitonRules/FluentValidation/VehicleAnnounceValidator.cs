using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class VehicleAnnounceValidator:AbstractValidator<VehicleAnnounceForCreationDto>
    {
        public VehicleAnnounceValidator()
        {
            RuleFor(x=>x.Header).NotEmpty().WithMessage("Araç ilanı başlığı boş olamaz...");
            RuleFor(x=>x.Header).MaximumLength(140).WithMessage("Araç ilanı başlığı en fazla 140 karakter olmalı...");
            RuleFor(x=>x.Description).MaximumLength(500).WithMessage("Araç ilanı açıklama en fazla 500 karakter olmalı");
            RuleFor(x=>x.VehicleCategoryId).NotEmpty().WithMessage("Araç ilanı için kategori seçiniz...");
            RuleFor(x=>x.VehicleBrandId).NotEmpty().WithMessage("Araç ilanı için Marka seçiniz...");
            RuleFor(x=>x.VehicleModelId).NotEmpty().WithMessage("Araç ilanı için Model  seçiniz...");
            RuleFor(x=>x.VehicleFuelTypeId).NotEmpty().WithMessage("Araç ilanı için yakıt tipi seçiniz...");
            RuleFor(x=>x.VehicleGearTypeId).NotEmpty().WithMessage("Araç ilanı için vites tipi seçiniz...");
            RuleFor(x=>x.VehicleEngineSizeId).NotEmpty().WithMessage("Araç ilanı için motor hacmi  seçiniz...");
            RuleFor(x=>x.UserId).NotEmpty().WithMessage("ilan için bir kullanıcı seçiniz...");
        }
    }
}