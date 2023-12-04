using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Offer;
using SASSTS.Model.Entities;

namespace SASSTS.WebAPI.Controllers
{
    [Route("offers")]
    [ApiController]
    public class OfferController : BaseController
    {
        private readonly IOfferBs _offerBs;
        public OfferController(IOfferBs offerBs) => _offerBs = offerBs;

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllOffers() => SendResponse(await _offerBs.GetAllAsync());

        //Onaylanmış teklifler yönetim veya kurula gidecek.
        [HttpGet("getallbycompanyIdOffers")]
        public async Task<IActionResult> GetAllbyCompanyId([FromQuery] int companyId, [FromQuery] int requestId) => SendResponse(
            await _offerBs.GetAllbyCompanyIdAsync(companyId, requestId, includeList: "Supplier"));

        [HttpPost("insertOffer")]
        public async Task<IActionResult> CreateOffer([FromBody] Offer offer) => SendResponse(await _offerBs.InsertAsync(offer));

        //SA birimleri teklif girme şekli hesabı
        [HttpPost("createOffers")]
        public async Task<IActionResult> CreateAll([FromBody] OfferPostDto dto)
        {
            var company = _offerBs.RandomUret(dto.CompanyId);

            await _offerBs.InsertAsync(new Offer
            {
                RequestId = dto.RequestId,
                SupplierId = dto.SupplierId,
                OfferDescription = dto.OfferDescription,
                OfferTime = DateTime.Now,
                OfferStatus = 0,
                OfferNo = company,
                OfferPrice = dto.OfferPrice,
                PriceCurrency = dto.PriceCurrency
            });

            return Ok("başarılı");
        }

        //Yönetim işleri için aşağıda olaylar dönüyor onaylama veya red etme vs.

        //Teklif onaylar.
        [HttpPut("successOffer")]
        public async Task<IActionResult> SuccessOffer([FromQuery] int offerId, [FromQuery] int requestId) => SendResponse(
            await _offerBs.SuccessOffer(offerId, requestId));

        //Teklif reddeder.
        [HttpPut("deleteOffer")]
        public async Task<IActionResult> DeleteOffer([FromQuery] int offerId) => SendResponse(await _offerBs.DeleteOffer(offerId));
    }
}