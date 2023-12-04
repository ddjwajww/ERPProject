using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Account;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Accounts
{
    public class RandomNumberAndEmailValidator : AbstractValidator<RandomNumberAndEmail>
    {
        public RandomNumberAndEmailValidator(LanguageService service)
        {

            RuleFor(x => x.RandomNumberNo)
                .NotNull().NotEmpty().WithMessage(service.Getkey("Güvenlik kodu boş bırakılamaz.").Value)
                .InclusiveBetween(102029, 969850).WithMessage(service.Getkey("Güvenlik kodu 6 karakterden oluşmalıdır.").Value);
        }
    }
}
