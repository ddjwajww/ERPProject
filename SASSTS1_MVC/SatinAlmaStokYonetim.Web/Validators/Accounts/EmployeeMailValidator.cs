using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Account;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Accounts
{
    public class EmployeeMailValidator : AbstractValidator<EmployeeMail>
    {
        public EmployeeMailValidator(LanguageService service) 
        {
            RuleFor(x => x.UserData)
                .NotEmpty().WithMessage(service.Getkey("ePosta bilgisi boş olamaz.").Value)
               .MinimumLength(5).WithMessage(service.Getkey("ePosta veya Kullanıcı Adı 5 karakterden büyük olmalıdır.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("ePosta bilgisi 50 karakterden büyük olamaz.").Value)
                .EmailAddress().WithMessage(service.Getkey("Geçerli bir ePosta adresi girmediniz.").Value);
        }
    }
}
