using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Business.RandomSystems;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.Company;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class CompanyBs : BusinessMap, ICompanyBs
    {
        private readonly IExceptionValidDto<Company, CompanyGetDto, CompanyBs, CompanyPostDto, CompanyPutDto> _companyBs;
        private readonly IRandomNumber _rm;
        public CompanyBs(IUnitWork unitWork, IExceptionValidDto<Company, CompanyGetDto, CompanyBs, CompanyPostDto, CompanyPutDto> companyBs, IRandomNumber rm) { _companyBs = companyBs; _rm = rm; }
        public async Task<ApiResponse<List<CompanyGetDto>>> GetAllAsync() => await _companyBs.GetAllDtosController("Company", x => x.IsDeleted == false);
        public async Task<ApiResponse<CompanyGetDto>> GetByCompNoAsync(string companyNo) => await _companyBs.GetbyFilter("Company", x => x.CompanyNo == companyNo);
        public async Task<ApiResponse<CompanyGetDto>> GetByIdAsync(byte id) => await _companyBs.GetbyFilter("Company", x => x.Id == id);
        public async Task<ApiResponse<NoData>> InsertCompany(CompanyPostDto dto) => await _companyBs.InsertAsyncNoData(dto, "Company", x => x.IsDeleted = false,
            x => x.CompanyName == dto.CompanyName || x.CompanyNo == dto.CompanyNo);
        public string RandomUret(int companyId, int departmentId) => _rm.randomUret(companyId, departmentId);
 
        public async Task<ApiResponse<NoData>> DeleteAsync(int id) => await _companyBs.DeleteAsync("Company", prd => prd.Id == id, mdl => mdl.IsDeleted = true);
        public async Task<ApiResponse<NoData>> UpdateAsync(CompanyPutDto dto) => await _companyBs.UpdateDto(dto, "Company", null!, x => x.CompanyName == dto.CompanyName
        && x.CompanyNo == dto.CompanyNo);
    }
}