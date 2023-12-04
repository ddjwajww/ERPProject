using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Account;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Accounts
{
    public class AccountItemValidator : AbstractValidator<AccountItem>
    {
        public AccountItemValidator(LanguageService service)
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage(service.Getkey("ePosta bilgisi boş olamaz.").Value)
               .MinimumLength(5).WithMessage(service.Getkey("ePosta bilgisi 5 karakterden büyük olmalıdır.").Value)
               .MaximumLength(50).WithMessage(service.Getkey("ePosta bilgisi 50 karakterden büyük olamaz.").Value)
               .EmailAddress().WithMessage(service.Getkey("Geçerli bir ePosta adresi girmediniz.").Value)
               .WithName(service.Getkey("ePosta").Value);

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(service.Getkey("Kullanıcı adı boş olamaz.").Value)
                .MinimumLength(5).WithMessage(service.Getkey("Kullanıcı adı bilgisi 5 karakterden büyük olmalıdır.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Kullanıcı adı bilgisi 50 karakterden büyük olamaz.").Value)
               .WithName(service.Getkey("Kullanıcı Adı").Value);

            RuleFor(x => x.UserPassword)
                .NotEmpty().WithMessage(service.Getkey("Şifre boş olamaz.").Value)
                .MinimumLength(8).WithMessage(service.Getkey("Şifre en az 8 karakter girilmelidir.").Value)
                .MaximumLength(16).WithMessage(service.Getkey("Şifre en fazla 16 karakter girilmelidir.").Value)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#])").WithMessage(service.Getkey("Şifre en az bir küçük harf, bir büyük harf, bir rakam ve bir özel karakter içermelidir.").Value)
               .WithName(service.Getkey("Şifre").Value);
        }
    }
}