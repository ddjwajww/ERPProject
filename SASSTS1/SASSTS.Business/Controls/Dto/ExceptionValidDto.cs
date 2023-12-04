using AutoMapper;
using Infrastructure.ApiResponses;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SASSTS.Business.CustomExceptions;
using SASSTS.DataAccess.UnitOfWork;
using System.Linq.Expressions;

namespace SASSTS.Business.Controls.Dto
{
    public static class ExceptionValidDto<T, A, C> where T : class, IEntity where A : class, IDto where C : class
    {
        //Mapleme yapılan ama include almayan GetAll
        public static async Task<ApiResponse<List<A>>> GetAllDtosController(IUnitWork unitWork, string table,
            IMapper mapper, Expression<Func<T, bool>> predicate = null!, ILogger<C> logger = null!, params string[] include)
        {
            logger.LogInformation($"{table} tablosuna ait veriler {DateTime.Now} tarihinde çekmeye çalışıldı");
            var models = await unitWork.GetRepository<T>().GetAllAsync(predicate, include);
            if (models.Count == 0)
                throw new NotFoundException($"{table} tablosuna ait veriler bulunamadı");
            var returnList = mapper.Map<List<A>>(models);
            if (returnList.Count > 0)
            {
                logger.LogInformation($"{table} tablosuna ait veriler {DateTime.Now} tairihinde çekildi");
                return ApiResponse<List<A>>.Success(StatusCodes.Status200OK, returnList);
            }
                throw new BadRequestException($"{table} tablosuna ait veriler bulunamadı");
        }

        //Filtreleme ile tek dto dönen mapleme işi yapılan.
        public static async Task<ApiResponse<A>> GetbyFilter(IUnitWork unitWork, string table, IMapper mapper, ILogger<C> logger = null!,
            Expression<Func<T, bool>> predicate = null!, params string[] include)
        {
            logger.LogInformation($"{table} tablosuna ait veriler çekmeye çalışıldı. zaman -> {DateTime.Now}");

            var model = await unitWork.GetRepository<T>().GetAsync(predicate);
            if(model != null)
            {
                var dto = mapper.Map<A>(model);
                logger.LogInformation($"{table} tablosuna ait veriler çekildi. zaman -> {DateTime.Now}");
                return ApiResponse<A>.Success(StatusCodes.Status200OK, dto);
            }
            throw new BadRequestException($"{table} tablosunda verilen sorguya uygun veri bulunamadı");
        }


        //Insert etme geriye veri döndüren method
        public static async Task<ApiResponse<T>> InsertAsync(A dto, IMapper mapper, IUnitWork unitWork, ILogger<C> logger)
        {
            var model = mapper.Map<T>(dto);
            var insertedModel = await unitWork.GetRepository<T>().InsertAsync(model);
            logger.LogInformation($"tabloya post etme işlemi gerçekleşti");
            return ApiResponse<T>.Success(StatusCodes.Status200OK, insertedModel);
        }

        public static async Task<ApiResponse<NoData>> InsertAsyncNoData(A dto, IMapper mapper, IUnitWork unitWork, ILogger<C> logger)
        {
            var model = mapper.Map<T>(dto);
            await unitWork.GetRepository<T>().InsertAsync(model);
            logger.LogInformation($"tabloya post etme işlemi gerçekleşti");
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}