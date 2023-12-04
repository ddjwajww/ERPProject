using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Supplier;
using SASSTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface ISupplierBs
    {
        Task<ApiResponse<List<SupplierGetDto>>> GetAllAsync();
        Task<ApiResponse<NoData>> Insert(SupplierPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(SupplierPutDto dto);
    }
}
