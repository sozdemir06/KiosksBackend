using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class AnnounceContentTypeValidator:AbstractValidator<AnnounceContentTypeForCreationDto>
    {
        public AnnounceContentTypeValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Duyuru içerik tipi boş olamaz..");
            RuleFor(x=>x.Description).NotEmpty().WithMessage("Duyuru içerik tipi boş olamaz..");
        }
    }
}