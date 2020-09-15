using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class RoleCategoryValidator : AbstractValidator<RoleCategoryForCreationAndUpdateDto>
    {
        public RoleCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Role Kategori Adı boş olamaz..");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Role Kategori Açıklaması boş olamaz..");
        }
    }
}