using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class PublicFooterTextValidator:AbstractValidator<PublicFooterTextForCreationDto>
    {
        public PublicFooterTextValidator()
        {
            RuleFor(x=>x.FooterText).NotEmpty().WithMessage("Footer Metni boş olamaz..");
        }
    }
}