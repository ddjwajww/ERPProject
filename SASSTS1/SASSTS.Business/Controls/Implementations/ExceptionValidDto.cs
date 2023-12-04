using AutoMapper;
using Infrastructure.ApiResponses;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SASSTS.Business.Controls.Dto;
using SASSTS.DataAccess.UnitOfWork;
using System.Linq.Expressions;

namespace SASSTS.Business.Controls.Implementations
{
    //T -> model,  A -> GetDto,   C -> Business Sınıfı,  D -> PostDto,   E -> PutDto
    public class ExceptionValidDto<T, A, C, D, E> : IExceptionValidDto<T, A, C, D, E> where T : class, IEntity where A : class, IDto where C : BusinessMap where D : class, IDto where E : class, IDto
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;
        private readonly ILogger<C> _logger;
        public ExceptionValidDto(IUnitWork unitWork, IMapper mapper, ILogger<C> logger)
        {
            _logger = logger;
            _unitWork = unitWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<A>>> GetAllDtosController(string table, Expression<Func<T, bool>> predicate = null!, params string[] includeList)
        {
            _logger.LogInformation($"{table} tablosundan veriler çekmeye çalışıldı");
            var models = await _unitWork.GetRepository<T>().GetAllAsync(predicate, includeList);
            if (models.Count == 0)
                return ApiResponse<List<A>>.Fail(StatusCodes.Status200OK, $"{table} tablosuna ait veriler bulunamadı");
            var returnList = _mapper.Map<List<A>>(models);
            if (returnList.Count > 0)
            {
                _logger.LogInformation($"{table} tablosuna ait veriler çekildi");
                return ApiResponse<List<A>>.Success(StatusCodes.Status200OK, returnList);
            }
            return ApiResponse<List<A>>.Fail(StatusCodes.Status200OK, $"{table} tablosuna ait veriler bulunamadı");
        }

        public async Task<ApiResponse<A>> GetbyFilter(string table, Expression<Func<T, bool>> predicate = null!, params string[] includeList)
        {
            _logger.LogInformation($"{table} tablosuna ait veriler çekmeye çalışıldı");

            var model = await _unitWork.GetRepository<T>().GetAsync(predicate, includeList);
            if (model != null)
            {
                var dto = _mapper.Map<A>(model);
                _logger.LogInformation($"{table} tablosuna ait veriler çekildi. zaman -> {DateTime.Now}");
                return ApiResponse<A>.Success(StatusCodes.Status200OK, dto);
            }
            throw new Exception("qdkddd");
        }

        public async Task<ApiResponse<T>> InsertAsync(D dto, string table)
        {
            var model = _mapper.Map<T>(dto);
            var insertedModel = await _unitWork.GetRepository<T>().InsertAsync(model);
            _logger.LogInformation($"{table} tablosuna post etme işlemi gerçekleşti");
            return ApiResponse<T>.Success(StatusCodes.Status200OK, insertedModel);

        }

        public async Task<ApiResponse<NoData>> InsertAsyncNoData(D dto, string table, Action<T> insertAction = null!, Expression<Func<T, bool>> anyExprr = null!)
        {
            if (anyExprr == null)
            {
                var model = _mapper.Map<T>(dto);
                if (insertAction != null)
                    insertAction(model);
                await _unitWork.GetRepository<T>().InsertAsync(model);
                _logger.LogInformation($"{table} tablosuna post etme işlemi gerçekleşti");
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new Exception("Eklenecek olan veri zaten mevcut!!!");
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(string table, Expression<Func<T, bool>> predicate, Action<T> updateActions = null!, Expression<Func<T, bool>> anyExprr = null!)
        {
            if (anyExprr == null)
            {
                var model = await _unitWork.GetRepository<T>().GetAsync(predicate);
                if (model == null)
                    return ApiResponse<NoData>.Fail(StatusCodes.Status200OK, $"{table} tablosunda güncelleme işlemi yapılamadı");
                if (updateActions != null)
                    updateActions(model);
                await _unitWork.GetRepository<T>().UpdateAsync(model);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new Exception("Eklenecek olan veri zaten mevcut!!!");
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(string table, Expression<Func<T, bool>> predicate, Action<T> updateAction = null!)
        {
            var model = await _unitWork.GetRepository<T>().GetAsync(predicate);
            if (model == null)
                return ApiResponse<NoData>.Fail(StatusCodes.Status200OK, "Silincek olan model bulunamadı");
            if (updateAction != null)
                updateAction(model);
            await _unitWork.GetRepository<T>().UpdateAsync(model);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

        }

        public async Task<ApiResponse<NoData>> UpdateDto(E dto, string table, Action<T> action, Expression<Func<T, bool>> anyExprr = null!)
        {
            if (anyExprr == null)
            {
                if (dto == null)
                    return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, $"Güncellenecek olan veri boş olmamalıdır -> tablo---{table}");
                var model = _mapper.Map<T>(dto);
                if (model == null)
                    return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, $"mapleme işlemi yapılamadı.  -> tablo---{table}");
                await _unitWork.GetRepository<T>().UpdateAsync(model);
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            throw new Exception("Eklenecek olan veri zaten mevcut!!!");
        }
    }
}
