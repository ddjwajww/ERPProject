using Infrastructure.ApiResponses;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.DataAccess.UnitOfWork;
using SASSTS.Model.Dtos.Todo;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class TodoBs : BusinessMap, ITodosBs
    {
        private readonly IExceptionValidDto<Todo, TodoGetDto, TodoBs, TodoPostDto, TodoPutDto> _todo;
        private readonly IUnitWork _unitWork;
        public TodoBs(IExceptionValidDto<Todo, TodoGetDto, TodoBs, TodoPostDto, TodoPutDto> todo, IUnitWork unitWork) { _todo = todo; _unitWork = unitWork;}

        public async Task<ApiResponse<List<TodoGetDto>>> GetallbyEmployeeIdAsync(long employeeId) => await _todo.GetAllDtosController("Todo", prd => prd.EmployeeId == employeeId && prd.IsDeleted == false);
        public async Task<ApiResponse<List<TodoGetDto>>> GetAllTodosAsync() => await _todo.GetAllDtosController("Todo", x => x.IsDeleted == false);
        public async Task<ApiResponse<NoData>> InsertAsync(TodoPostDto dto) => await _todo.InsertAsyncNoData(dto, "Todo", x => x.IsDeleted = false);
        public async Task<ApiResponse<NoData>> UpdateAsync(TodoPutDto updateDto) => await _todo.UpdateAsync("Todo", prd => prd.Id == updateDto.Id, mdl => mdl.Active = updateDto.Active);
        public async Task<ApiResponse<NoData>> DeleteAsync(int[] dizi)
        {
            foreach (var number in dizi)
            {
                var todo = await _unitWork.GetRepository<Todo>().GetAsync(x => x.Id == number);
                todo.IsDeleted = true;
                await _unitWork.GetRepository<Todo>().UpdateAsync(todo);
            }
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}