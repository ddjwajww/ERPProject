using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Category;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Categories
{
    public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidator(LanguageService service)
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage(service.Getkey("Kategori adı boş bırakılamaz.").Value)
                .MinimumLength(5).WithMessage(service.Getkey("Kategori adı en az 5 karakter olabilir.").Value)
                .MaximumLength(100).WithMessage(service.Getkey("Kategori adı en fazla 100 karakter olabilir.").Value);
        }
    }
}
