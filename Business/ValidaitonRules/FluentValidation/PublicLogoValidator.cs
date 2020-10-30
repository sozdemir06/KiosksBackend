using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class PublicLogoValidator : AbstractValidator<PublicLogoForCreationDto>
    {
        public PublicLogoValidator()
        {
           
            RuleFor(x => x.Name).NotEmpty().WithMessage("Dosya adı boş olamaz...");
            RuleFor(x => x.FullPath).NotEmpty().WithMessage("Dosya tam adresi boş olamaz...");
        }
    }
}