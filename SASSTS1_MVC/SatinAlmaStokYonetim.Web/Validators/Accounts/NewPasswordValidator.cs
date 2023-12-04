using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Account;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Accounts
{
    public class NewPasswordValidator : AbstractValidator<NewPassword>
    {
        public NewPasswordValidator(LanguageService service) 
        {
            RuleFor(x => x.UserPassword)
                .NotEmpty().WithMessage(service.Getkey("Şifre boş olamaz.").Value)
                .MinimumLength(8).WithMessage(service.Getkey("Şifre en az 8 karakter girilmelidir.").Value)
                .MaximumLength(16).WithMessage(service.Getkey("Şifre en fazla 16 karakter girilmelidir.").Value)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#])").WithMessage(service.Getkey("Şifre en az bir küçük harf, bir büyük harf, bir rakam ve bir özel karakter içermelidir.").Value);
        }
    }
}
