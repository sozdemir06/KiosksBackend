using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class AnnouncesubScreenValidator : AbstractValidator<AnnounceSubScreenForCreationDto>
    {
        public AnnouncesubScreenValidator()
        {
            RuleFor(x => x.AnnounceId).NotEmpty().WithMessage("İlan ID boş olamaz...");
            RuleFor(x => x.SubScreenId).NotEmpty().WithMessage("Alt Ekran ID boş olamaz...");
        }
    }
}