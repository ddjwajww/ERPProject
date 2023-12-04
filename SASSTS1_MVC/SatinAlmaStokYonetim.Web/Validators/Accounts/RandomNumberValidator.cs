using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Accounts
{
    public class RandomNumberValidator : AbstractValidator<RandomNumber>
    {
        public RandomNumberValidator(LanguageService service)
        {
            RuleFor(x => x.Number)
                .NotEmpty().NotNull().WithMessage(service.Getkey("Güvenlik kodu boş bırakılamaz.").Value)
                .InclusiveBetween(102029, 969850).WithMessage(service.Getkey("Güvenlik kodu 6 karakterden oluşmalıdır.").Value);
        }
    }
}
