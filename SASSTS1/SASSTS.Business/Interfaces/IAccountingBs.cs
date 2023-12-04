using Infrastructure.ApiResponses;
using SASSTS.Business.Security.JWT;
using SASSTS.Model.Dtos.Accounting;
using SASSTS.Model.Dtos.RandomNumberDtos;
using SASSTS.Model.Dtos.TokenDtos;
using SASSTS.Model.Entities;
using SASSTS.Model.RequestModels.AccountingVM;
using SASSTS.Model.RequestModels.EmployeeVM;

namespace SASSTS.Business.Interfaces
{
    public interface IAccountingBs
    {
        Task<ApiResponse<EmployeeandAccountModel>> GetAccountbyAccountingId(int accountingId, string include);
        Task<ApiResponse<List<AccountingGetDto>>> GetAccounting(string include);
        Task<ApiResponse<int>> ForgottenPassword(EmployeeMailVM employeeMailVM);
        Task<ApiResponse<long>> RandomNumberConfirm(RandomNumberNoVM randomNumberNoVM);
        Task<ApiResponse<bool>> UpdateUser(UpdateUserVM updateUserVM);
        Task<ApiResponse<bool>> UpdateEmployee(UpdateEmployeeVM updateEmployeeVM);
        Task<ApiResponse<bool>> NewPassword(NewPasswordVM newPasswordVM);
        Task<ApiResponse<Model.Entities.RandomNumber>> LoginTwoFactor(LoginVM loginVM);
        Task<ApiResponse<AccessToken>> RandomNumberLogin(RandomNumberNoVM randomNumberNoVM);
        Task<ApiResponse<bool>> DeleteRandomNumber(NewPasswordVM newPasswordVM);
        Task<ApiResponse<bool>> DeleteRandomNumber(RandomNumberNoVM randomNumberNoVM);
    }
}