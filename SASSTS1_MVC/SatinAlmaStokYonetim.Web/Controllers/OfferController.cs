using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SatinAlmaStokYonetim.Web.ApiServices;
using SatinAlmaStokYonetim.Web.Models.IdValue;
using SatinAlmaStokYonetim.Web.Models.Offer;
using SatinAlmaStokYonetim.Web.Models.Request;
using SatinAlmaStokYonetim.Web.Models.Response;
using System.Text.Json;

namespace SatinAlmaStokYonetim.Web.Controllers
{
    public class OfferController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IValidator<OfferPostDto> _validatorOfferPostDto;
        public OfferController(IHttpApiService httpApiService, IValidator<OfferPostDto> validatorOfferPostDto)
        {
            _httpApiService = httpApiService;
            _validatorOfferPostDto = validatorOfferPostDto;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer([FromBody] OfferPostDto dto) =>
            await _httpApiService.PostData<NoData>("offers/createOffers", JsonSerializer.Serialize(dto));

        [HttpPost]//Bu onaylanmış teklifleri çekiyor.
        public async Task<IActionResult> GetAllOffers([FromBody] CompandRequestId item) =>
            await _httpApiService.GetData<List<OfferGetDto>>($"offers/getallbycompanyIdOffers?companyId={item.CompanyId}&requestId={item.RequestId}");

        [HttpPost]//Gelen offerId değerine göre onay verme işlemi
        public async Task<IActionResult> SuccessOffer([FromBody] RequestOffer item) =>
            await _httpApiService.PutData<NoData>($"offers/successOffer?offerId={item.OfferId}&requestId={item.RequestId}", JsonSerializer.Serialize(item));

        [HttpPost] //Teklif red etme
        public async Task<IActionResult> DeleteOffer([FromBody] int offerId) =>
            await _httpApiService.PutData<NoData>($"offers/deleteOffer?offerId={offerId}", JsonSerializer.Serialize(offerId));
    }
}