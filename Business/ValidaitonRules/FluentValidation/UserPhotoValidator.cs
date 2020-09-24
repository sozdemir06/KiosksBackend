using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class UserPhotoValidator:AbstractValidator<UserPhoto>
    {
        public UserPhotoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Foroğraf'ın adı boş olamaz...");
            RuleFor(x => x.FullPath).NotEmpty().WithMessage("Foroğraf'ın tam adresi boş olamaz...");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı ID boş olamaz...");  
        }
    }
}