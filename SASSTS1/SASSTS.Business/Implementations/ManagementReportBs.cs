using Infrastructure.ApiResponses;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.ManagementReport;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class ManagementReportBs : BusinessMap, IManagementReportBs
    {
        private readonly IExceptionValidDto<ManagementReport, ManagementReportGetDto, ManagementReportBs, ManagementReportPostDto, ManagementReportGetDto> _mr;
        private readonly IUnitWork _unitWork;
        public ManagementReportBs(IExceptionValidDto<ManagementReport, ManagementReportGetDto, ManagementReportBs, ManagementReportPostDto, ManagementReportGetDto> mr, IUnitWork unitWork) { _mr = mr; _unitWork = unitWork; }

        public async Task<ApiResponse<List<ManagementReportGetDto>>> GetAllAsync() => await _mr.GetAllDtosController("ManagementReport");

        public async Task<ApiResponse<MRGetDto>> GetallbyCompanyId(int companyId)
        {
            var reports = await _unitWork.GetRepository<ManagementReport>().GetAllAsync(x => x.CompanyId == companyId && x.IsRead == false);
            if (reports == null && reports!.Count == 0)
                throw new Exception("raporlar bulunamadı");
            MRGetDto mRGetDto = new MRGetDto();
            List<DetailProducts> detailProducts = new List<DetailProducts>();
            foreach (var report in reports)
            {

                mRGetDto.CompanyId = report.CompanyId;
                mRGetDto.SupplierCompanyName = report.SupplierCompanyName;
                mRGetDto.CompanyMail = report.CompanyMail;
                mRGetDto.CompanyPhone = report.CompanyPhone;
                mRGetDto.CreateTime = report.CreateTime;
                mRGetDto.RequestNo = report.RequestNo;
                mRGetDto.PriceCurrency = report.PriceCurrency;
                mRGetDto.Price = report.Price;
                mRGetDto.Id = report.Id;
                mRGetDto.EmployeeCompanyName = report.EmployeeCompanyName;
                mRGetDto.EmployeeDepartmentName = report.EmployeeDepartmentName;
                mRGetDto.EmployeeName = report.EmployeeName;
                mRGetDto.EmployeeSurname = report.EmployeeSurname;

                detailProducts.Add(new DetailProducts()
                {
                    CategoryName = report.CategoryName,
                    ProductName = report.ProductName,
                    ProductQuantity = report.ProductQuantity
                });
                mRGetDto.DetailProducts = detailProducts;
            }
            if (mRGetDto != null)
                return ApiResponse<MRGetDto>.Success(statusCode: StatusCodes.Status200OK, mRGetDto);
            throw new Exception("rapor bulunamadı");
        }

        public async Task<ApiResponse<NoData>> UpdateRead(int reportId)
        {
            var report = await _unitWork.GetRepository<ManagementReport>().GetAsync(x => x.Id == reportId);
            if (report == null)
                return ApiResponse<NoData>.Fail(StatusCodes.Status200OK, "Rapor bulunamadı");
            var reports = await _unitWork.GetRepository<ManagementReport>().GetAllAsync(x => x.RequestNo == report.RequestNo);
            if (reports.Count == 0)
                return ApiResponse<NoData>.Fail(StatusCodes.Status200OK, "Raporlar bulunamadı");
            foreach (var r in reports)
            {
                r.IsRead = true;
                await _unitWork.GetRepository<ManagementReport>().UpdateAsync(r);
            }
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}