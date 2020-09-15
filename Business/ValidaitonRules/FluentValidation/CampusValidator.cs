using Core.Entities.Concrete;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class CampusValidator:AbstractValidator<CampuseForCreationDto>
    {
        public CampusValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Campus adı boş olamaz");
            RuleFor(x=>x.Name).MaximumLength(140).WithMessage("Campus adı en fazla 140 karakter olmalı");
        }
    }
}