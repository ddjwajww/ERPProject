using FluentValidation;
using Microsoft.Extensions.Localization;
using SatinAlmaStokYonetim.Web.Models.Employee;
using SatinAlmaStokYonetim.Web.Services;

namespace SatinAlmaStokYonetim.Web.Validators.Employees
{
    public class EmployeePostDtoValidator : AbstractValidator<EmployeePostDto>
    {
        public EmployeePostDtoValidator(LanguageService service)
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage(service.Getkey("Rol bilgisi boş bırakılamaz.").Value)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.AuthorityId)
                .NotEmpty().WithMessage(service.Getkey("Yetki bilgisi boş bırakılamaz.").Value)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage(service.Getkey("Şirket bilgisi boş bırakılamaz.").Value)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(service.Getkey("Departman bilgisi boş bırakılamaz.").Value)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.EmployeeName)
                .NotEmpty().WithMessage(service.Getkey("Personel adı boş bırakılamaz.").Value)
                .MinimumLength(3).WithMessage(service.Getkey("Personel adı en az 3 karakterden oluşabilir.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Personel adı en fazla 50 karakterden oluşabilir.").Value);

            RuleFor(x => x.EmployeeSurname)
                .NotEmpty().WithMessage(service.Getkey("Personel soyadı boş bırakılamaz.").Value)
                .MinimumLength(3).WithMessage(service.Getkey("Personel soyadı en az 3 karakterden oluşabilir.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Personel soyadı en fazla 50 karakterden oluşabilir.").Value);

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage(service.Getkey("Personel telefon numarası boş bırakılamaz.").Value)
                .Length(11).WithMessage(service.Getkey("Personel telefon numarası 11 karakterden oluşmalıdır.").Value);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(service.Getkey("Personel e-posta adresi boş bırakılamaz.").Value)
                .MaximumLength(50).WithMessage(service.Getkey("Personel e-posta adresi en fazla 50 karakterden oluşabilir.").Value)
                .EmailAddress().WithMessage(service.Getkey("Geçerli bir eposta adresi girmediniz.").Value);

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage(service.Getkey("Personel fotografı boş bırakılamaz.").Value);

            RuleFor(x => x.IdentityNo)
                .NotEmpty().WithMessage(service.Getkey("Personel TC kimlik numarası boş bırakılamaz.").Value)
                .Length(11).WithMessage(service.Getkey("Personel TC kimlik numarası 11 karakterden oluşmalıdır.").Value);
        }
    }
}