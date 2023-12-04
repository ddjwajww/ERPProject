using Infrastructure.ApiResponses;
using Infrastructure.Models;
using System.Linq.Expressions;

namespace SASSTS.Business.Controls.Dto
{
    public interface IExceptionValidDto<T, A, C, D, E> where T : class, IEntity where A : class, IDto where C : class where D : class, IDto where E : class, IDto
    {
        Task<ApiResponse<List<A>>> GetAllDtosController(string table, Expression<Func<T, bool>> predicate = null!, params string[] includeList);
        Task<ApiResponse<A>> GetbyFilter(string table, Expression<Func<T, bool>> predicate = null!, params string[] includeList);
        Task<ApiResponse<T>> InsertAsync(D dto, string table);
        Task<ApiResponse<NoData>> InsertAsyncNoData(D dto, string table, Action<T> insertAction = null!, Expression<Func<T, bool>> anyExprr = null!);
        Task<ApiResponse<NoData>> UpdateAsync(string table, Expression<Func<T, bool>> predicate, Action<T> updateAction = null!, Expression<Func<T, bool>> anyExprr = null!);
        Task<ApiResponse<NoData>> DeleteAsync(string table, Expression<Func<T, bool>> predicate, Action<T> updateAction = null!);
        Task<ApiResponse<NoData>> UpdateDto(E dto, string table, Action<T> action, Expression<Func<T, bool>> anyExprr = null!);
    }
}
