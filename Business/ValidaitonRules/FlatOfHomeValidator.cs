using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules
{
    public class FlatOfHomeValidator:AbstractValidator<FlatOfHomeForCreationDto>
    {
        public FlatOfHomeValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Opsiyon adı boş olamaz...");
            RuleFor(x=>x.Name).MaximumLength(60).WithMessage("Opsiyon adı en fazla 60 karakter olmalı...");
        }
    }
}