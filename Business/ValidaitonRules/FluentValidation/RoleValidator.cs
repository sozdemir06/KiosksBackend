using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class RoleValidator:AbstractValidator<RoleForCreationAndUpdateDto>
    {
        public RoleValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Role Adı boş olamaz..");
            RuleFor(x=>x.Description).NotEmpty().WithMessage("Role Açıklaması boş olamaz..");
            RuleFor(x=>x.RoleCategoryId).NotEmpty().WithMessage("Role için bir kategori belirtiniz...");
        }
    }
}