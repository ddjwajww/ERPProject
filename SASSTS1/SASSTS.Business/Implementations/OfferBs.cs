using AutoMapper;
using Infrastructure.ApiResponses;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SASSTS.Business.Attributes;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.CustomExceptions;
using SASSTS.Business.Interfaces;
using SASSTS.Business.RandomSystems;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.Offer;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class OfferBs : BusinessMap, IOfferBs
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OfferBs> _logger;
        private readonly IRandomNumber _rm;
        public OfferBs(IMapper mapper, IUnitWork unitWork, ILogger<OfferBs> logger, IRandomNumber rm) {_mapper = mapper; _unitWork = unitWork;
            _logger = logger; _rm = rm;
        }
        public async Task<ApiResponse<List<OfferGetDto>>> GetAllAsync() =>
            await ExceptionValidDto<Offer, OfferGetDto, OfferBs>.GetAllDtosController(_unitWork, "Offer", _mapper, logger: _logger);



        //5k altı şu an için yönetime gidiyor daha sonra kur hesabı felan yapılacak oynamayın, aleşmeyin!!. Thanks.
        //hangi talebe karşılık yapılan teklif ise o listeleniyor.
        public async Task<ApiResponse<List<OfferGetDto>>> GetAllbyCompanyIdAsync(int companyId, int requestId, params string[] includeList)
        {
            var company = await _unitWork.GetRepository<Company>().GetAsync(x => x.Id == (byte)companyId);
            var offers = await _unitWork.GetRepository<Offer>().GetAllAsync(x => x.OfferNo.Contains(company.CompanyNo) && x.RequestId == requestId, includeList: includeList);
            var returnList = _mapper.Map<List<OfferGetDto>>(offers);
            return ApiResponse<List<OfferGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }


         
        public async Task<ApiResponse<NoData>> InsertAsync(Offer offer)
        {
            var off = await _unitWork.GetRepository<Offer>().InsertAsync(offer);
            var time = await _unitWork.GetRepository<TimeReport>().GetAsync(x => x.RequestId == (byte)offer.RequestId);
            time.SADate = DateTime.Now;
            await _unitWork.GetRepository<TimeReport>().UpdateAsync(time);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
        public string RandomUret(int companyId) => _rm.randomUretCompany(companyId);
        //Bu da teklif onay verme
        public async Task<ApiResponse<NoData>> SuccessOffer(int offerId, int requestId)
        {
            //onaylanan dışındakiler default olarak red edilmiş gibi gösteriliyor...
            var requests = await _unitWork.GetRepository<Offer>().GetAllAsync(x => x.RequestId == requestId);
            foreach (var request in requests)
            {
                request.OfferStatus = 2;
                await _unitWork.GetRepository<Offer>().UpdateAsync(request);
            }

            var offer = await _unitWork.GetRepository<Offer>().GetAsync(x => x.Id == offerId);
            offer.OfferStatus = 1;  //Onay verme eylemi
            await _unitWork.GetRepository<Offer>().UpdateAsync(offer);

            var time = await _unitWork.GetRepository<TimeReport>().GetAsync(x => x.RequestId == offer.RequestId);
            time.ManagementDate = DateTime.Now;
            await _unitWork.GetRepository<TimeReport>().UpdateAsync(time);

            var requesT = await _unitWork.GetRepository<Request>().GetAsync(x => x.Id == requestId);
            requesT.RequestStatus = 4;
            await _unitWork.GetRepository<Request>().UpdateAsync(requesT);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
        //Bu eylem teklif red
        public async Task<ApiResponse<NoData>> DeleteOffer(int offerId)
        {
            var offer = await _unitWork.GetRepository<Offer>().GetAsync(x => x.Id == offerId);
            offer.OfferStatus = 2;  //Red verme eylemi
            await _unitWork.GetRepository<Offer>().UpdateAsync(offer);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}