using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class NumberOfRoomValidator:AbstractValidator<NumberOfRoomForCreateOrUpdateDto>
    {
        public NumberOfRoomValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Adı boş olamaz...");
            RuleFor(x=>x.Name).MaximumLength(60).WithMessage("Adı en fazla 60 karakter olmalı...");
        }
    }
}