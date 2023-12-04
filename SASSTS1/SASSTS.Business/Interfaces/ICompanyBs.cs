using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Company;
using SASSTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface ICompanyBs
    {
        Task<ApiResponse<List<CompanyGetDto>>> GetAllAsync();
        Task<ApiResponse<CompanyGetDto>> GetByIdAsync(byte id);
        Task<ApiResponse<CompanyGetDto>> GetByCompNoAsync(string companyNo);
        string RandomUret(int companyId, int departmentId);
        Task<ApiResponse<NoData>> InsertCompany(CompanyPostDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<NoData>> UpdateAsync(CompanyPutDto dto);
    }
}
