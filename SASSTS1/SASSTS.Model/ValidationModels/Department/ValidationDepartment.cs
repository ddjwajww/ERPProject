using FluentValidation;
using SASSTS.Model.Dtos.Department;

namespace SASSTS.Model.ValidationModels.Department
{
    public class ValidationDepartment : AbstractValidator<DepartmentPostDto>
    {
        public ValidationDepartment()
        {
            RuleFor(x => x.DepartmentName).NotNull().WithMessage("DepartmentName boş bırakılamaz");
            RuleFor(x => x.DepartmentNo).MaximumLength(3).WithMessage("DepartmentNo max 3 karakter olmalıdır.");
        }
    }
}