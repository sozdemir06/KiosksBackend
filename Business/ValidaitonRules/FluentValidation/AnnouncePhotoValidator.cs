using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class AnnouncePhotoValidator : AbstractValidator<AnnouncePhotoForCretionDto>
    {
        public AnnouncePhotoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Foroğraf'ın adı boş olamaz...");
            RuleFor(x => x.FullPath).NotEmpty().WithMessage("Foroğraf'ın tam adresi boş olamaz...");
            RuleFor(x => x.AnnounceId).NotEmpty().WithMessage("Fotoğraf'ın hangi ilana ait olduğunu belirtiniz...");
        }
    }
}