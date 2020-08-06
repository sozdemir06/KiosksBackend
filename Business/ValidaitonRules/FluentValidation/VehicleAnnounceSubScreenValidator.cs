using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class VehicleAnnounceSubScreenValidator : AbstractValidator<VehicleAnnounceSubScreenForCreationDto>
    {
        public VehicleAnnounceSubScreenValidator()
        {
            RuleFor(x => x.VehicleAnnounceId).NotEmpty().WithMessage("İlan ID boş olamaz...");
            RuleFor(x => x.SubScreenId).NotEmpty().WithMessage("Alt Ekran ID boş olamaz...");
        }
    }
}