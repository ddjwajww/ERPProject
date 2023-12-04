using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Role;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Roles
{
    public class RolePostDtoValidator : AbstractValidator<RolePostDto>
    {
        public RolePostDtoValidator(LanguageService service)
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(service.Getkey("Rol bilgisi boş bırakılamaz.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Rol adı en fazla 50 karakterden oluşabilir.").Value)
                .MinimumLength(5).WithMessage(service.Getkey("Rol adı en az 5 karakterden oluşabilir.").Value);
        }
    }
}
