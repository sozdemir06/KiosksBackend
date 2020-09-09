using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class ScreenHeaderPhotoValidator:AbstractValidator<ScreenHeaderPhotoForCreationDto>
    {
        public ScreenHeaderPhotoValidator()
        {
            RuleFor(x=>x.Position).NotEmpty().WithMessage("Logo pozisyonunu belirtiniz...");
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Dosya adı boş olamaz...");
            RuleFor(x=>x.FullPath).NotEmpty().WithMessage("Dosya tam adresi boş olamaz...");
            RuleFor(x=>x.ScreenId).NotEmpty().WithMessage("Ekran belirtiniz...");
        }
    }
}