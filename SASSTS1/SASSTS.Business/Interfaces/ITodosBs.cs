using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Todo;

namespace SASSTS.Business.Interfaces
{
    public interface ITodosBs
    {
        Task<ApiResponse<List<TodoGetDto>>> GetallbyEmployeeIdAsync(long employeeId);
        Task<ApiResponse<List<TodoGetDto>>> GetAllTodosAsync();
        Task<ApiResponse<NoData>> InsertAsync(TodoPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(TodoPutDto updateDto);
        Task<ApiResponse<NoData>> DeleteAsync(int[] dizi);
    }
}
