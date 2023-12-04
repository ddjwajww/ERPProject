using AutoMapper;
using Infrastructure.ApiResponses;
using Microsoft.AspNetCore.Http;
using SASSTS.Business.Interfaces;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.Support;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class SupportBs : ISupportBs
    {
        private readonly IMapper _mapper;
        private readonly IUnitWork _unitWork;
        public SupportBs(IMapper mapper, IUnitWork unitWork)
        {
            _mapper = mapper;
            _unitWork = unitWork;
        }
        public async Task<ApiResponse<List<Support>>> GetAll()
        {
            var supports = await _unitWork.GetRepository<Support>().GetAllAsync();
            if(supports == null && supports.Count == 0) 
                throw new Exception("Supports bulunamadı");
            return ApiResponse<List<Support>>.Success(statusCode: StatusCodes.Status200OK, supports);
            
        }

        public async Task<ApiResponse<NoData>> InsertSupport(SupportPostDto dto)
        {
            var model = _mapper.Map<Support>(dto);
            if(model != null)
            {
                await _unitWork.GetRepository<Support>().InsertAsync(model);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new Exception("Eklenecek olan veri bulunamadı");
        }
    }
}
