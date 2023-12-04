using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.BasketDetail;
using SASSTS.Model.Dtos.Request;
using SASSTS.Model.Entities;

namespace SASSTS.WebAPI.Controllers
{
    [Route("baskets")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketBs _basketBs;
        private readonly IBasketDetailBs _basketDetailBs;
        private readonly IRequestBs _requestBs;
        private readonly ICompanyBs _companyBs;
        public BasketController(IBasketBs basketBs, IBasketDetailBs basketDetailBs, IRequestBs requestBs, ICompanyBs companyBs)
        {
            _basketBs = basketBs;
            _basketDetailBs = basketDetailBs;
            _requestBs = requestBs;
            _companyBs = companyBs;
        }
        [HttpGet("getBaskets")]
        public async Task<IActionResult> GetBaskets() => SendResponse(await _basketBs.GetAllBaskets());

        [HttpPost("insertBasket")]
        public async Task<IActionResult> Create([FromBody] RequestAllModelDto dto)
        {
            var basket = await _basketBs.InsertBasket(new Model.Dtos.Basket.BasketPostDto { CreatedTime = DateTime.Now});
            
            foreach(var item in dto.Basket)
            {
                await _basketDetailBs.Insert(new BasketDetailPostDto
                {
                    BasketId = basket.Data!.Id,
                    ProductId = (int)item.ProductId,
                    Quantity = (int)item.ProductQuantity,
                    UnitQuantity = item.ProductUnitQuantity,
                    DetailStatus = 0
                });
            }
            var no = _companyBs.RandomUret((int)dto.CompanyId, (int)dto.DepartmentId);

            await _requestBs.InsertRequest(new Request
            {
                BasketId= (int)basket.Data!.Id,
                EmployeeId = dto.UserId,
                //requestno için random repoyu çağır.
                RequestNo = no,
                RequestStatus = 0,
            });

            return Ok("Başarılı");
        }
    }
}