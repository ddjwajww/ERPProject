using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Product;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Products
{
    public class ProductItemValidator : AbstractValidator<ProductItem>
    {
        public ProductItemValidator(LanguageService service)
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage(service.Getkey("Kategori bilgisi boş bırakılamaz.").Value);

            RuleFor(x => x.UnitId)
                .NotEmpty().WithMessage(service.Getkey("Birim bilgisi boş bırakılamaz.").Value);

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage(service.Getkey("Ürün adı boş bırakılamaz.").Value)
                .MinimumLength(2).WithMessage(service.Getkey("Ürün adı en az 2 karakterden oluşabilir.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Ürün adı en fazla 50 karakterden oluşabilir.").Value);

            RuleFor(x => x.ProductImage)
                .NotEmpty().WithMessage(service.Getkey("Ürün resim bilgisi boş bırakılamaz.").Value);
        }
    }
}