using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class OnlineUserValidator:AbstractValidator<OnlineUser>
    {
        public OnlineUserValidator()
        {
            RuleFor(x=>x.UserId).NotEmpty().WithMessage("Kullanıcı ID boş olamaz...");
            RuleFor(x=>x.ConnectionId).NotEmpty().WithMessage("Kullanıcı ConnectionID boş olamaz...");
        }
    }
}