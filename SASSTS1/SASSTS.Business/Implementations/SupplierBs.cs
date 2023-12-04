using Infrastructure.ApiResponses;
using Infrastructure.Models;
using SASSTS.Business.Attributes;
using SASSTS.Business.Controls.Dto;
using SASSTS.Business.Interfaces;
using SASSTS.Model.Dtos.Supplier;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Implementations
{
    public class SupplierBs : BusinessMap, ISupplierBs
    {
        private readonly IExceptionValidDto<Supplier, SupplierGetDto, SupplierBs, SupplierPostDto, SupplierPutDto> _spl;
        public SupplierBs(IExceptionValidDto<Supplier, SupplierGetDto, SupplierBs, SupplierPostDto, SupplierPutDto> spl) => _spl = spl;
        public async Task<ApiResponse<List<SupplierGetDto>>> GetAllAsync() => await _spl.GetAllDtosController("Supplier");
        public async Task<ApiResponse<NoData>> Insert(SupplierPostDto dto) => await _spl.InsertAsyncNoData(dto, "Supplier");
        public async Task<ApiResponse<NoData>> Delete(int id) => await _spl.DeleteAsync("Supplier", prd => prd.Id == id, mdl => mdl.IsDeleted = true);
        public async Task<ApiResponse<NoData>> UpdateAsync(SupplierPutDto dto) => await _spl.UpdateDto(dto, "Supplier", null!);
    }
}