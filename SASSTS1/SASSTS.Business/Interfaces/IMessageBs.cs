using Infrastructure.ApiResponses;
using SASSTS.Model.Dtos.Message;
using SASSTS.Model.Entities;

namespace SASSTS.Business.Interfaces
{
    public interface IMessageBs
    {
        //Task<ApiResponse<Message>> InsertAsync(MessagePostDto message);
        Task<ApiResponse<NoData>> InsertAllMessage(MessagePostDto dto);
        Task<ApiResponse<List<MessageGetDto>>> GetAllMessage();
        Task<ApiResponse<List<MessageGetDto>>> GetAllMessageEmployeebyAuthorityId(int authorityId);
    }
}
