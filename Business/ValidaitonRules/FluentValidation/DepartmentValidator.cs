using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class DepartmentValidator:AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Birim ID boş olamaz");
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Birim adı boş olamaz");
            RuleFor(x=>x.Name).MaximumLength(140).WithMessage("Birim adı en fazla 140 karakter olmalı");
        }
    }
}