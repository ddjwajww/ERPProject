using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Account;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Accounts
{
    public class AccountModelValidator : AbstractValidator<AccountModel>
    {
        public AccountModelValidator(LanguageService service)
        {
            RuleFor(x => x.userName)
                .NotEmpty().WithMessage(service.Getkey("Kullanıcı adı boş olamaz.").Value)
               .MinimumLength(5).WithMessage(service.Getkey("Kullanıcı adı bilgisi 5 karakterden büyük olmalıdır.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Kullanıcı adı bilgisi 50 karakterden büyük olamaz.").Value);

            RuleFor(x => x.userPassword)
                .NotEmpty().WithMessage(service.Getkey("Şifre boş olamaz.").Value)
               .MinimumLength(8).WithMessage(service.Getkey("Şifre en az 8 karakter girilmelidir.").Value)
                .MaximumLength(25).WithMessage(service.Getkey("Şifre en fazla 16 karakter girilmelidir.").Value)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#])").WithMessage(service.Getkey("Şifre en az bir küçük harf, bir büyük harf, bir rakam ve bir özel karakter içermelidir.").Value);
        }
    }
}
