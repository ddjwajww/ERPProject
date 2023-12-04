using Infrastructure.ApiResponses;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using SASSTS.Business.Attributes;
using SASSTS.Business.CustomExceptions;
using SASSTS.Business.Interfaces;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class RequestBs : BusinessMap, IRequestBs
    {
        private readonly IUnitWork _unitWork;
        public RequestBs(IUnitWork unitWork) =>
            _unitWork = unitWork;
        public async Task<ApiResponse<List<Request>>> GetAllRequests(int companyId)
        {
            var company = await _unitWork.GetRepository<Company>().GetAsync(x => x.Id == (byte)companyId);
            var requests = await _unitWork.GetRepository<Request>().GetAllAsync(x => x.RequestNo.Contains(company.CompanyNo) && x.RequestStatus == 3);
            return ApiResponse<List<Request>>.Success(StatusCodes.Status200OK, requests);
        }

        public async Task<ApiResponse<NoData>> InsertRequest(Request model)
        {
            var re = await _unitWork.GetRepository<Request>().InsertAsync(model);
            var querry = model.RequestNo.Substring(0, 6);
            var company = await _unitWork.GetRepository<Company>().GetAsync(x => x.CompanyNo.Contains(querry));
            await _unitWork.GetRepository<TimeReport>().InsertAsync(new TimeReport
            {
                RequestId = (int)re.Id,
                AccountDate = DateTime.Now,
                ManagementDate = DateTime.Now,
                RequestDate = DateTime.Now,
                SADate = DateTime.Now,
                StockDate = DateTime.Now,
                SuccessDate = DateTime.Now,
                CompanyId = (int)company.Id,
                IsRead = false
            });
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
        public async Task<ApiResponse<List<Request>>> GetbyCompanyNo(int companyId, int departmentId)
        {
            var company = await _unitWork.GetRepository<Company>().GetAsync(x => x.Id == (byte)companyId);
            var department = await _unitWork.GetRepository<Department>().GetAsync(x => x.Id == (byte)departmentId);

            var requests = await _unitWork.GetRepository<Request>().GetAllAsync(x => x.RequestStatus == 0 && x.RequestNo.Contains(company.CompanyNo)
            && x.RequestNo.Contains(department.DepartmentNo));

            return ApiResponse<List<Request>>.Success(StatusCodes.Status200OK, requests);
        }
        public async Task<ApiResponse<List<Request>>> GetbySA(int companyId, int departmentId)
        {
            var company = await _unitWork.GetRepository<Company>().GetAsync(x => x.Id == (byte)companyId);
            var department = await _unitWork.GetRepository<Department>().GetAsync(x => x.Id == (byte)departmentId);

            var requests = await _unitWork.GetRepository<Request>().GetAllAsync(x => x.RequestStatus == 1 && x.RequestNo.Contains(company.CompanyNo)
            && x.RequestNo.Contains(department.DepartmentNo));

            return ApiResponse<List<Request>>.Success(StatusCodes.Status200OK, requests);
        }
        public async Task<ApiResponse<Request>> GetbyIdAsync(long id)
        {
            if (id <= 0)
                throw new BadRequestException("id değeri 0 dan küçük veya eşit olamaz");
            var request = await _unitWork.GetRepository<Request>().GetAsync(x => x.Id == id);
            if (request != null)
                return ApiResponse<Request>.Success(StatusCodes.Status200OK, request);
            throw new NotFoundException("Girilen id değerine uygun veri bulunamadı");
        }

        //Onay verme işlemi 
        public async Task<ApiResponse<NoData>> UpdateAsync(long id)
        {
            var request = await _unitWork.GetRepository<Request>().GetAsync(x => x.Id == id);
            request.RequestStatus = 1;
            var time = await _unitWork.GetRepository<TimeReport>().GetAsync(x => x.RequestId == request.Id);
            time.SuccessDate = DateTime.Now;
            await _unitWork.GetRepository<TimeReport>().UpdateAsync(time);
            await _unitWork.GetRepository<Request>().UpdateAsync(request);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
        public async Task<ApiResponse<NoData>> DeleteAsync(long id)
        {
            var request = await _unitWork.GetRepository<Request>().GetAsync(x => x.Id == id);
            request.RequestStatus = 2;
            await _unitWork.GetRepository<Request>().UpdateAsync(request);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
        public async Task<ApiResponse<NoData>> RequestStatusTwo(int id)
        {
            var request = await _unitWork.GetRepository<Request>().GetAsync(x => x.Id == (long)id);
            request.RequestStatus = 3;
            await _unitWork.GetRepository<Request>().UpdateAsync(request);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
        public async Task<ApiResponse<List<Request>>> GetStatusFour(int companyId, int departmentId)
        {
            var company = await _unitWork.GetRepository<Company>().GetAsync(x => x.Id == (byte)companyId);
            var department = await _unitWork.GetRepository<Department>().GetAsync(x => x.Id == (byte)departmentId);

            var requests = await _unitWork.GetRepository<Request>().GetAllAsync(x => x.RequestStatus == 4 && x.RequestNo.Contains(company.CompanyNo) && x.RequestNo.Contains(department.DepartmentNo));

            return ApiResponse<List<Request>>.Success(StatusCodes.Status200OK, requests);
        }

        public async Task<ApiResponse<List<Request>>> GetAllRequestsNoStatus(int companyId)
        {
            var company = await _unitWork.GetRepository<Company>().GetAsync(x => x.Id == (byte)companyId);
            var requests = await _unitWork.GetRepository<Request>().GetAllAsync(x => x.RequestNo.Contains(company.CompanyNo));
            return ApiResponse<List<Request>>.Success(StatusCodes.Status200OK, requests);
        }

    }
}
