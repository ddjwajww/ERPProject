using FluentValidation;
using SatinAlmaStokYonetim.Web.Models;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Accounts
{
    public class LoginValidator : AbstractValidator<LoginVM>
    {
        public LoginValidator(LanguageService service)
        {
            RuleFor(x => x.UserData)
                .MinimumLength(5).WithMessage(service.Getkey("Kullanıcı Adı veya ePosta adresi en az 5 karakter girilmelidir.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Kullanıcı Adı veya ePosta adresi en fazla 50 karakter girilmelidir.").Value);

            RuleFor(x => x.UserPassword)
                .NotEmpty().WithMessage(service.Getkey("Şifre boş olamaz.").Value)
                .MinimumLength(8).WithMessage(service.Getkey("Şifre en az 8 karakter girilmelidir.").Value)
                .MaximumLength(16).WithMessage(service.Getkey("Şifre en fazla 16 karakter girilmelidir.").Value)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#])").WithMessage(service.Getkey("Şifre en az bir küçük harf, bir büyük harf, bir rakam ve bir özel karakter içermelidir.").Value);
        }

    }
}
