using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Company;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Companies
{
    public class CompanyPostDtoValidator : AbstractValidator<CompanyPostDto>
    {
        public CompanyPostDtoValidator(LanguageService service)
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage(service.Getkey("Şirket adı boş bırakılamaz.").Value)
                .MinimumLength(5).WithMessage(service.Getkey("Şirket adı en az 5 karakterden oluşmalıdır.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Şirket adı en fazla 50 karakterden oluşabilir.").Value);

            RuleFor(x => x.CompanyNo)
                .NotEmpty().WithMessage(service.Getkey("Şirket numarası boş bırakılamaz.").Value)
                .Length(6).WithMessage(service.Getkey("Şirket numarası 6 karakterden oluşabilir.").Value);
        }
    }
}
