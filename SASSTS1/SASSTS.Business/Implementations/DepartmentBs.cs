using Infrastructure.ApiResponses;
using Infrastructure.FluentValidation;
using Infrastructure.Models;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Department;
using SASSTS.Model.Entities;
using SASSTS.Model.ValidationModels.Department;

namespace SASSTS.Business.Implementations
{
    public class DepartmentBs : BusinessMap, IDepartmentBs
    {
        private readonly IExceptionValidDto<Department, DepartmentGetDto, DepartmentBs, DepartmentPostDto, DepartmentPutDto> _dp;
        public DepartmentBs(IExceptionValidDto<Department, DepartmentGetDto, DepartmentBs, DepartmentPostDto, DepartmentPutDto> dp) => _dp = dp;
        public async Task<ApiResponse<List<DepartmentGetDto>>> GetAllAsync() => await _dp.GetAllDtosController("Department", x => x.IsDeleted == false);
        public async Task<ApiResponse<DepartmentGetDto>> GetById(byte id) => await _dp.GetbyFilter("Department", prd => prd.Id == id);
        [ValidationBehavior(typeof(ValidationDepartment))]
        public async Task<ApiResponse<NoData>> InsertDepartment(DepartmentPostDto dto) => await _dp.InsertAsyncNoData(dto, "Department", 
            x => x.IsDeleted = false, x => x.DepartmentName == dto.DepartmentName || x.DepartmentNo == dto.DepartmentNo);
        public async Task<ApiResponse<NoData>> DeleteAsync(int id) => await _dp.DeleteAsync("Department", prd => prd.Id == id, mdl => mdl.IsDeleted = true);
        public async Task<ApiResponse<NoData>> UpdateAsync(DepartmentPutDto dto) => await _dp.UpdateDto(dto, "Department", null!,
            x => x.DepartmentNo == dto.DepartmentNo && x.DepartmentName == dto.DepartmentName);
    }
}