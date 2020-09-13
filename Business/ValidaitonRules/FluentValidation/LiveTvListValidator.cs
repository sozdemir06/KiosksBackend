using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class LiveTvListValidator:AbstractValidator<LiveTvList>
    {
        public LiveTvListValidator()
        {
            RuleFor(x=>x.TvName).NotEmpty().WithMessage("Televiz yon adı boş olamaz...");
            RuleFor(x=>x.YoutubeId).NotEmpty().WithMessage("Youtube video ID boş olanaz..");
        }
    }
}