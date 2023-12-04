using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Offer;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Offers
{
    public class OfferPostDtoValidator : AbstractValidator<OfferPostDto>
    {
        public OfferPostDtoValidator(LanguageService service)
        {
            RuleFor(x => x.RequestId)
                .NotEmpty().WithMessage(service.Getkey("Talep bilgisi boş bırakılamaz.").Value);

            RuleFor(x => x.SupplierId)
                .NotEmpty().WithMessage(service.Getkey("Tedarikçi bilgisi boş bırakılamaz.").Value);

            RuleFor(x => x.OfferPrice)
                .NotEmpty().WithMessage(service.Getkey("Teklif tutarı boş bırakılamaz.").Value)
                .GreaterThan(0).WithMessage(service.Getkey("Teklif tutarı sıfırdan büyük bir değer olmalıdır.").Value);

            RuleFor(x=>x.PriceCurrency)
                .NotEmpty().WithMessage(service.Getkey("Para birimi bilgisi boş bırakılamaz.").Value);
        }
    }
}