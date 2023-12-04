using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Bill;
using SatinAlmaStokYonetim.Web.Services;
using System;

namespace SatinAlmaStokYonetim.Web.Validators.Bills
{
    public class BillModelValidator : AbstractValidator<BillModel>
    {
        public BillModelValidator(LanguageService service)
        {
            RuleFor(x => x.RequestId)
                .NotEmpty().WithMessage(service.Getkey("Talep bilgisi boş bırakılamaz.").Value);

            RuleFor(x=>x.SupplierId)
                .NotEmpty().WithMessage(service.Getkey("Tedarikçi bilgisi boş bırakılamaz.").Value);

            RuleFor(x=>x.BillType)
                .NotEmpty().WithMessage(service.Getkey("Fatura tipi bilgisi boş bırakılamaz.").Value);

            RuleFor(x=>x.BillDate)
                .NotEmpty().WithMessage(service.Getkey("Fatura tarih bilgisi boş bırakılamaz.").Value);

            RuleFor(x => x.BillNumber)
                .NotEmpty().WithMessage(service.Getkey("Fatura numara bilgisi boş bırakılamaz.").Value)
                .Matches(@"^[a-zA-Z][a-zA-Z0-9][0-9]{14}$").WithMessage(service.Getkey("Fatura numarası, bir harf ile başlamalı ve sonraki 15 karakter rakam olmalıdır.").Value)
                .Length(16).WithMessage(service.Getkey("Fatura numarası 16 karakterden oluşmalıdır.").Value);

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage(service.Getkey("İndirim tutarı sıfır veya daha büyük bir tutar olmalıdır.").Value);

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage(service.Getkey("Para birimi bilgisi boş bırakılamaz.").Value);

        }
    }
}