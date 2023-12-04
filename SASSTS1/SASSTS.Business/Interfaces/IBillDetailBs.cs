using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.BillDetail;
using SASSTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASSTS.Business.Interfaces
{
    public interface IBillDetailBs
    {
        Task<ApiResponse<List<BillDetailGetDto>>> GetallDetail(int billId);
    }
}
