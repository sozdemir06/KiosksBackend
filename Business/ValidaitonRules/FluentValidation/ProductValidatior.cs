using Entities.Concrete;
using FluentValidation;

namespace Business.ValidaitonRules.FluentValidation
{
    public class ProductValidatior : AbstractValidator<Product>
    {
        public ProductValidatior()
        {
            RuleFor(p=>p.ProductName).NotEmpty().WithMessage("Ürün adı boş olamaz...");
            RuleFor(p=>p.ProductName).Length(2,30).WithMessage("Ürün adı enaz 2 en fazla 30 karakter olmalı..");
        }
    }
}