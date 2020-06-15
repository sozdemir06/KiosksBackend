using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class CampusValidator:AbstractValidator<Campus>
    {
        public CampusValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Campus ID boş olamaz");
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Campus adı boş olamaz");
            RuleFor(x=>x.Name).MaximumLength(140).WithMessage("Campus adı en fazla 140 karakter olmalı");
        }
    }
}