using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class NotifyGroupValidation:AbstractValidator<NotifyGroupForCreationDto>
    {
        public NotifyGroupValidation()
        {
            RuleFor(x=>x.GroupName).NotEmpty().WithMessage("Bildirim group adı boş olamaz..");
            RuleFor(x=>x.Description).NotEmpty().WithMessage("Bildirim açıklaması boş olamaz..");
        }
    }
}