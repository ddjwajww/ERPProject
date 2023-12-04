using FluentValidation;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using SatinAlmaStokYonetim.Web.Models.Authority;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Authorities
{
    public class AuthorityPostDtoValidator : AbstractValidator<AuthorityPostDto>
    {
        public AuthorityPostDtoValidator(LanguageService service) 
        {
            RuleFor(x => x.AuthorityName)
                .NotEmpty().WithMessage(service.Getkey("Yetki adı boş bırakılamaz.").Value)
                .MinimumLength(5).WithMessage(service.Getkey("Yetki adı en az 5 karakterden oluşmalıdır.").Value)
                .MaximumLength(30).WithMessage(service.Getkey("Yetki adı en fazla 50 karakterden oluşabilir.").Value);
        }
    }
}
