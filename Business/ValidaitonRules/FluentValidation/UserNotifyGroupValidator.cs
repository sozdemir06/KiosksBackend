using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class UserNotifyGroupValidator:AbstractValidator<UserNotifyGroupForCreationDto>
    {
        public UserNotifyGroupValidator()
        {
            RuleFor(x=>x.UserId).NotEmpty().WithMessage("Kullanıcı ID boş olamaz...");
            RuleFor(x=>x.NotifyGroupId).NotEmpty().WithMessage("Bildirim Group ID boş olamaz...");
        }
    }
}