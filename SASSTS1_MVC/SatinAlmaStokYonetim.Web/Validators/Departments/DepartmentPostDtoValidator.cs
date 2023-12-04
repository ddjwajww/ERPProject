using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Department;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Departments
{
    public class DepartmentPostDtoValidator : AbstractValidator<DepartmentPostDto>
    {
        public DepartmentPostDtoValidator(LanguageService service)
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage(service.Getkey("Departman adı boş bırakılamaz.").Value)
                .MinimumLength(5).WithMessage(service.Getkey("Departman adı en az 5 karakterden oluşmalıdır.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Departman adı en fazla 50 karakterden oluşabilir.").Value);

            RuleFor(x => x.DepartmentNo)
                .NotEmpty().WithMessage(service.Getkey("Departman numarası boş bırakılamaz.").Value)
                .Length(3).WithMessage(service.Getkey("Departman numarası 3 karakterden oluşabilir.").Value);
        }
    }
}
