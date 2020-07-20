using Entities.Dtos;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class HomeAnnounceValidator:AbstractValidator<HomeAnnounceForCreationDto>
    {
        public HomeAnnounceValidator()
        {
            RuleFor(x=>x.Header).NotEmpty().WithMessage("Ev ilanı başlığı boş olamaz...");
            RuleFor(x=>x.Header).MaximumLength(140).WithMessage("Ev ilanı başlığı en fazla 140 karakter olmalı...");
            RuleFor(x=>x.Description).MaximumLength(500).WithMessage("Ev ilanı açıklama en fazla 500 karakter olmalı");
            RuleFor(x=>x.FlatOfHomeId).NotEmpty().WithMessage("Ev ilanı için bulunduğu katı seçiniz...");
            RuleFor(x=>x.HeatingTypeId).NotEmpty().WithMessage("Ev ilanı için ısıtma tipini seçiniz...");
            RuleFor(x=>x.NumberOfRoomId).NotEmpty().WithMessage("Ev ilanı için oda sayısı seçiniz...");
            RuleFor(x=>x.BuildingAgeId).NotEmpty().WithMessage("Yapının yaşını seçiniz...");
            RuleFor(x=>x.UserId).NotEmpty().WithMessage("ilan için bir kullanıcı seçiniz...");
        }
    }
}