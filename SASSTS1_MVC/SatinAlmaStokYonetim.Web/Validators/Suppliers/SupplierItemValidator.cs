using FluentValidation;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using SatinAlmaStokYonetim.Web.Models.Supplier;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Suppliers
{
    public class SupplierItemValidator : AbstractValidator<SupplierItem>
    {
        public SupplierItemValidator(LanguageService service)
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage(service.Getkey("Tedarikçi adı boş bırakılamaz.").Value)
                .MinimumLength(5).WithMessage(service.Getkey("Tedarikçi adı en az 5 karakterden oluşabilir.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Tedarikçi adı en fazla 50 karakterden oluşabilir.").Value);

            RuleFor(x => x.CompanyMail)
                .NotEmpty().WithMessage(service.Getkey("Tedarikçi e-posta adresi boş bırakılamaz.").Value)
                .MinimumLength(8).WithMessage(service.Getkey("Tedarikçi e-posta adresi en az 5 karakterden oluşabilir.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Tedarikçi e-posta adresi en fazla 50 karakterden oluşabilir.").Value)
                .EmailAddress().WithMessage(service.Getkey("Geçerli bir eposta adresi girmediniz.").Value);

            RuleFor(x => x.CompanyPhone)
                .NotEmpty().WithMessage(service.Getkey("Tedarikçi telefon numarası boş bırakılamaz.").Value)
                .Length(11).WithMessage(service.Getkey("Tedarikçi telefon numarası en fazla 11 karakterden oluşabilir.").Value);
        }
    }
}
