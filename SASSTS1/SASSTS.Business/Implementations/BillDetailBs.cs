using Infrastructure.ApiResponses;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using SASSTS.Business.Attributes;
using SASSTS.Business.CustomExceptions;
using SASSTS.Business.Interfaces;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.BillDetail;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class BillDetailBs : BusinessMap, IBillDetailBs
    {
        private readonly IUnitWork _unitWork;
        public BillDetailBs(IUnitWork unitWork) => _unitWork = unitWork;
        public async Task<ApiResponse<List<BillDetailGetDto>>> GetallDetail(int billId)
        {
            if (billId <= 0)
                throw new BadRequestException("id değeri 0 dan küçük veya eşit olamaz");

            try
            {
                var details = await _unitWork.GetRepository<BillDetail>().GetAllAsync(x => x.BillId == billId);
                var dtos = new List<BillDetailGetDto>();

                foreach (var detail in details)
                {
                    var product = await _unitWork.GetRepository<Product>().GetAsync(x => x.Id == detail.ProductId);

                    dtos.Add(new BillDetailGetDto
                    {
                        BillId = detail.BillId,
                        Id = (int)detail.Id,
                        ProductKdv = detail.ProductKdv,
                        ProductName = product.ProductName,
                        ProductQuantity = detail.ProductQuantity,
                        UnitPrice = detail.UnitPrice
                    });
                }
                return ApiResponse<List<BillDetailGetDto>>.Success(StatusCodes.Status200OK, dtos);
            }
            catch (Exception ex)
            {
                throw new NoContentException(ex.Message == null ? "İşlem gerçeklşemedi ve hata mesajı boş" : "İşlem gerçekleşemedi Exception içeriğine bakınız");
            }
        }
    }
}