using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class LiveTvBroadCastSubsCreenValidator:AbstractValidator<LiveTvBroadCastSubScreen>
    {
        public LiveTvBroadCastSubsCreenValidator()
        {
             RuleFor(x=>x.LiveTvBroadCastId).NotEmpty().WithMessage("Tv Yayın ID boş olamaz...");
            RuleFor(x=>x.SubScreenId).NotEmpty().WithMessage("Alt Ekran ID boş olamaz...");
        }
    }
}